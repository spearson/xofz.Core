namespace xofz.Framework.Daq.Internal
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    internal sealed class CyUsbDio32Finder
    {
        public MaterializedEnumerable<Dio32> Find()
        {
            NativeMethods.ClearDevices();

            uint deviceIndex;
            var dio32s = new LinkedList<Dio32>();
            byte eePromByte;
            for (eePromByte = 0; eePromByte < 0xFF; ++eePromByte)
            {
                deviceIndex = NativeMethods.GetDeviceByEEPROMByte(
                    eePromByte);

                if (deviceIndex < 0xFFFFFFFF)
                {
                    dio32s.AddLast(new CyUsbDio32(
                        deviceIndex, eePromByte));
                }
            }

            // test at 0xFF
            deviceIndex = NativeMethods.GetDeviceByEEPROMByte(
                eePromByte);
            if (deviceIndex < 0xFFFFFFFF)
            {
                dio32s.AddLast(new CyUsbDio32(
                    deviceIndex, eePromByte));
            }

            if (dio32s.Count == 0)
            {
                this.searchUsingGetDevicesFunction(dio32s);
            }

            return new LinkedListMaterializedEnumerable<Dio32>(
                dio32s);
        }

        private void searchUsingGetDevicesFunction(LinkedList<Dio32> ll)
        {
            var bitMask = NativeMethods.GetDevices();
            for (var currentIndex = 0; currentIndex < 32; ++currentIndex)
            {
                if ((bitMask >> currentIndex) % 2 != 1)
                {
                    continue;
                }

                if (currentIndex == 0)
                {
                    ll.AddLast(new CyUsbDio32(
                        0, 0));
                    continue;
                }

                var deviceIndex = (uint)1 << currentIndex;
                ll.AddLast(new CyUsbDio32(
                    deviceIndex, 0));
            }
        }

        public MaterializedEnumerable<Dio32> FindWithOutputs(
            Dio32Ports outputs)
        {
            var dio32s = this.Find();
            foreach (var dio32 in dio32s)
            {
                dio32.Configure(
                    dio32.ReadOnTerminals(),
                    outputs);
            }

            return dio32s;
        }
    }
}
