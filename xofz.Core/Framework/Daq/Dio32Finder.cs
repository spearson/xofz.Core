namespace xofz.Framework.Daq
{
    using xofz.Framework.Daq.Internal;

    public class Dio32Finder
    {
        public Dio32Finder()
        {
            this.winUsbFinder = new WinUsbDio32Finder();
            this.cyUsbFinder = new CyUsbDio32Finder();
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
            return this.cyUsbFinder.Find();
        }

        public virtual MaterializedEnumerable<Dio32> FindCyUsbWithOutputs(
            Dio32Ports outputs = Dio32Ports.A | Dio32Ports.B | Dio32Ports.C)
        {
            return this.cyUsbFinder.FindWithOutputs(outputs);
        }

        private readonly WinUsbDio32Finder winUsbFinder;
        private readonly CyUsbDio32Finder cyUsbFinder;
    }
}
