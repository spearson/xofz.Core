namespace xofz.Framework.Plc
{
    using System.IO;
    using xofz.Framework.Plc.Implementation;

    public class AbPlcFactory
    {
        public AbPlcFactory()
        {
            this.file1Name = "MfgControl.AdvancedHMI.Drivers.dll";
            this.file2Name = "AdvancedHMIDrivers.dll";
            this.file3Name = "MfgControl.AdvancedHMI.Controls.dll";
            this.file4Name = "AdvancedHMIControls.dll";
        }

        public virtual AbPlc NewEthernet(string ipAddress)
        {
            var fn1AlreadyExists = true;
            var fn2AlreadyExists = true;
            var fn3AlreadyExists = true;
            var fn4AlreadyExists = true;

            lock (globalLock)
            {
                var fn = this.file1Name;
                if (!File.Exists(fn))
                {
                    fn1AlreadyExists = false;
                    File.Create(fn).Dispose();
                }

                fn = this.file2Name;
                if (!File.Exists(fn))
                {
                    fn2AlreadyExists = false;
                    File.Create(fn).Dispose();
                }

                fn = this.file3Name;
                if (!File.Exists(fn))
                {
                    fn3AlreadyExists = false;
                    File.Create(fn).Dispose();
                }

                fn = this.file4Name;
                if (!File.Exists(fn))
                {
                    fn4AlreadyExists = false;
                    File.Create(fn).Dispose();
                }

                var plc = new EthernetAbPlc(
                    ipAddress);

                fn = this.file1Name;
                if (!fn1AlreadyExists)
                {
                    File.Delete(fn);
                }

                fn = this.file2Name;
                if (!fn2AlreadyExists)
                {
                    File.Delete(fn);
                }

                fn = this.file3Name;
                if (!fn3AlreadyExists)
                {
                    File.Delete(fn);
                }

                fn = this.file4Name;
                if (!fn4AlreadyExists)
                {
                    File.Delete(fn);
                }

                return plc;
            }
        }

        private readonly string file1Name;
        private readonly string file2Name;
        private readonly string file3Name;
        private readonly string file4Name;
        private static readonly object globalLock
            = new object();
    }
}
