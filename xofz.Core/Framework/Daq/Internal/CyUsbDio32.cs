namespace xofz.Framework.Daq.Internal
{
    internal sealed class CyUsbDio32 : Dio32
    {
        string Dio32.DeviceLocation { get; }

        string Dio32.ReadSerialNumber()
        {
            throw new System.NotImplementedException();
        }

        Dio32Terminals Dio32.ReadOnTerminals()
        {
            throw new System.NotImplementedException();
        }

        void Dio32.WriteTerminals(Dio32Terminals newOnTerminals)
        {
            throw new System.NotImplementedException();
        }

        void Dio32.Configure(Dio32Terminals onTerminals, Dio32Ports outputs)
        {
            throw new System.NotImplementedException();
        }
    }
}
