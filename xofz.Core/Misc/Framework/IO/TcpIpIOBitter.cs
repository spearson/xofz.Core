namespace xofz.Misc.Framework.IO
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Threading;
    using xofz.Framework;
    using static EnumerableHelpers;

    public sealed class TcpIpIOBitter 
        : IOBitter
    {
        public TcpIpIOBitter(
            MethodWeb web)
        {
            this.web = web;
        }

        string IOBitter.Name { get; set; }

        public void Setup(
            string host, 
            int port)
        {
            if (Interlocked.Exchange(
                ref this.setupIf1,
                1) == 1)
            {
                return;
            }

            IOBitter bitter = this;
            var w = this.web;
            w.RegisterDependency(
                new BinaryTranslator());
            w.RegisterDependency(
                new TcpIpIOBitterSettings(),
                bitter.Name);
            w.Run<TcpIpIOBitterSettings>(
                settings =>
                {
                    settings.ReadBufferLength = 1024 * 1024 * 7;
                    settings.IPAddressOrHostName = host;
                    settings.Port = port;
                    settings.ReadTimeoutMilliseconds = 2000;
                    settings.WriteTimeoutMilliseconds = 2500;
                    settings.ConnectTimeoutMilliseconds = 2000;
                },
                bitter.Name);
            w.RegisterDependency(
                this,
                bitter.Name);
        }

        IEnumerable<bool> IOBitter.Read()
        {
            IOBitter bitter = this;
            var w = this.web;
            var bits = Empty<bool>();
            w.Run<TcpIpIOBitterSettings, BinaryTranslator>(
                (settings, bt) =>
                {
                    using (var client = new TcpClient())
                    {
                        try
                        {
                            if (client
                                .BeginConnect(
                                    settings.IPAddressOrHostName,
                                    settings.Port,
                                    result => { },
                                    new object())
                                .AsyncWaitHandle
                                .WaitOne(settings.ConnectTimeoutMilliseconds))
                            {
                                using (var stream = client.GetStream())
                                {
                                    stream.ReadTimeout
                                        = settings.ReadTimeoutMilliseconds;
                                    var buffer = new byte[
                                        settings.ReadBufferLength];
                                    var numberOfBytesRead = stream.Read(
                                        buffer,
                                        0,
                                        buffer.Length);

                                    var readBytes = new byte[
                                        numberOfBytesRead];
                                    Array.Copy(buffer,
                                        readBytes,
                                        numberOfBytesRead);
                                    bits = bt.GetBits(readBytes);
                                }
                            }
                        }
                        catch
                        {
                            bits = Empty<bool>();
                        }
                    }
                },
                bitter.Name);

            return bits;
        }

        void IOBitter.Write(
            IEnumerable<bool> bits, 
            out bool succeeded)
        {
            if (bits == default(IEnumerable<bool>))
            {
                succeeded = true;
                return;
            }

            IOBitter bitter = this;
            var w = this.web;
            var reallySucceeded = false;
            w.Run<TcpIpIOBitterSettings, BinaryTranslator>(
                (settings, bt) =>
                {
                    using (var client = new TcpClient())
                    {
                        try
                        {
                            if (client
                                .BeginConnect(
                                    settings.IPAddressOrHostName,
                                    settings.Port,
                                    result => { },
                                    new object())
                                .AsyncWaitHandle
                                .WaitOne(2000))
                            {
                                var bytes = ToArray(bt
                                    .GetBytes(bits));
                                using (var stream = client.GetStream())
                                {
                                    stream.WriteTimeout =
                                        settings.WriteTimeoutMilliseconds;
                                    stream.Write(
                                        bytes,
                                        0,
                                        bytes.Length);
                                    reallySucceeded = true;
                                }
                            }
                        }
                        catch
                        {
                            reallySucceeded = false;
                        }
                    }
                },
                bitter.Name);

            succeeded = reallySucceeded;
        }

        private long setupIf1;
        private readonly MethodWeb web;
    }
}
