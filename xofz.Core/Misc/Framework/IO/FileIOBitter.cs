namespace xofz.Misc.Framework.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using xofz.Framework;
    using static EnumerableHelpers;

    public sealed class FileIOBitter 
        : IOBitter
    {
        public FileIOBitter(
            MethodWeb web)
        {
            this.web = web;
        }

        string IOBitter.Name { get; set; }

        public void Setup(string startFilePath)
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
                new FileIOBitterSettings(),
                bitter.Name);
            w.Run<FileIOBitterSettings>(
                settings =>
                {
                    settings.FilePath = startFilePath;
                },
                bitter.Name);
            w.RegisterDependency(
                this,
                bitter.Name);
        }

        IEnumerable<bool> IOBitter.Read()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return Empty<bool>();
            }

            var w = this.web;
            IEnumerable<bool> bits = Empty<bool>();
            IOBitter bitter = this;
            w.Run<FileIOBitterSettings, BinaryTranslator>(
                (settings, bt) =>
                {
                    IEnumerable<byte> bytesToRead;
                    try
                    {
                        bytesToRead = File.ReadAllBytes(
                            settings.FilePath);
                    }
                    catch
                    {
                        return;
                    }

                    bits = bt.GetBits(bytesToRead);
                },
                bitter.Name);

            return bits;
        }

        void IOBitter.Write(
            IEnumerable<bool> bits, 
            out bool succeeded)
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                succeeded = false;
                return;
            }

            if (bits == default(IEnumerable<bool>))
            {
                succeeded = true;
                return;
            }

            var w = this.web;
            var reallySucceeded = false;
            IOBitter bitter = this;
            w.Run<FileIOBitterSettings, BinaryTranslator>(
                (settings, bt) =>
                {
                    var bytesToWrite = bt.GetBytes(bits);
                    var array = ToArray(bytesToWrite);
                    try
                    {
                        File.WriteAllBytes(
                            settings.FilePath,
                            array);
                    }
                    catch
                    {
                        reallySucceeded = false;
                        goto end;
                    }

                    reallySucceeded = true;
                    end:;
                },
                bitter.Name);

            succeeded = reallySucceeded;
        }

        private long setupIf1;
        private readonly MethodWeb web;
    }
}
