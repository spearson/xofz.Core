namespace xofz.Framework.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public sealed class FileIOBitter : IOBitter
    {
        public FileIOBitter(MethodWeb web)
        {
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            w.RegisterDependency(
                new BinaryTranslator());
        }

        IEnumerable<bool> IOBitter.Read(string location)
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return Enumerable.Empty<bool>();
            }

            var w = this.web;
            IEnumerable<bool> bits = Enumerable.Empty<bool>();
            w.Run<BinaryTranslator>(bt =>
            {
                IEnumerable<byte> bytesToRead;
                try
                {
                    bytesToRead = File.ReadAllBytes(location);
                }
                catch
                {
                    return;
                }
                
                bits = bt.GetBits(bytesToRead);
            });

            return bits;
        }

        void IOBitter.Write(
            IEnumerable<bool> bits, 
            string location, 
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
            IEnumerable<byte> bytesToWrite = default(IEnumerable<byte>);
            w.Run<BinaryTranslator>(bt =>
            {
                bytesToWrite = bt.GetBytes(bits);
            });

            var array = bytesToWrite.ToArray();
            try
            {
                File.WriteAllBytes(
                    location,
                    array);
            }
            catch
            {
                succeeded = false;
                return;
            }

            succeeded = true;
        }

        private long setupIf1;
        private readonly MethodWeb web;
    }
}
