namespace xofz.Framework.Plc.Implementation
{
    using System;
    using AdvancedHMIDrivers;
    using xofz.Internal;

    internal sealed class EthernetAbPlc : AbPlc
    {
        static EthernetAbPlc()
        {
            lock (EmbeddedAssemblyLoader.GlobalLock)
            {
                AppDomain.CurrentDomain.AssemblyResolve
                    -= EmbeddedAssemblyLoader.Load;
                AppDomain.CurrentDomain.AssemblyResolve
                    += EmbeddedAssemblyLoader.Load;
            }
        }

        public EthernetAbPlc(string ipAddress)
        {
            this.driver = new EthernetIPforSLCMicroCom
            {
                IPAddress = ipAddress
            };
        }

        public string Location => this.driver.IPAddress;

        string AbPlc.Read(string address)
        {
            return this.driver.Read(address);
        }

        void AbPlc.Write(string address, string value)
        {
            this.driver.Write(address, value);
        }

        private readonly EthernetIPforSLCMicroCom driver;
    }
}
