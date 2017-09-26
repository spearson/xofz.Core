namespace xofz.Framework.Daq
{
    using System;
    using xofz.Framework.Daq.Internal;

    public class Dio32Finder
    {
        public Dio32Finder()
        {
            this.winUsbFinder = new WinUsbDio32Finder();
        }

        public virtual MaterializedEnumerable<Dio32> FindWinUsb()
        {
            return this.winUsbFinder.Find();
        }

        public virtual MaterializedEnumerable<Dio32> FindWinUsbWithOutputs(
            Dio32Ports outputs = Dio32Ports.A | Dio32Ports.B | Dio32Ports.C)
        {
            return this.winUsbFinder.FindWithOutputs(outputs);
        }

        public virtual MaterializedEnumerable<Dio32> FindCyUsb()
        {
            throw new NotImplementedException(
                "DIO-32 access via the CyUSB driver not supported yet. :(");
        }

        public virtual MaterializedEnumerable<Dio32> FindCyUsbWithOutputs(
            Dio32Ports outputs = Dio32Ports.A | Dio32Ports.B | Dio32Ports.C)
        {
            throw new NotImplementedException(
                "DIO-32 access via the CyUSB driver not supported yet. :(");
        }

        private readonly WinUsbDio32Finder winUsbFinder;
    }
}
