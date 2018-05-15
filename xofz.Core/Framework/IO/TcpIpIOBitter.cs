namespace xofz.Framework.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    public sealed class TcpIpIOBitter : IOBitter
    {
        public TcpIpIOBitter(
            MethodWeb web)
        {
            this.web = web;
        }

        string IOBitter.Name { get; set; }

        public void Setup(string host, int port)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
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
                    settings.ReadBufferLength = 65536;
                    settings.IPAddressOrHostName = host;
                    settings.Port = port;
                    settings.ReadTimeoutMilliseconds = 2000;
                    settings.WriteTimeoutMilliseconds = 2500;
                    settings.ConnectTimeoutMilliseconds = 2000;
                },
                bitter.Name);
        }

        IEnumerable<bool> IOBitter.Read()
        {
            IOBitter bitter = this;
            var w = this.web;
            var bits = Enumerable.Empty<bool>();
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
                            bits = Enumerable.Empty<bool>();
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
                                var bytes = bt
                                    .GetBytes(bits)
                                    .ToArray();
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

        private int setupIf1;
        private readonly MethodWeb web;
    }
}
