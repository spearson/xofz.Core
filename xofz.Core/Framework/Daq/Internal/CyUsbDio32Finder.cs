namespace xofz.Framework.Daq.Internal
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    internal sealed class CyUsbDio32Finder
    {
        public MaterializedEnumerable<Dio32> Find()
        {
            var dio32s = new LinkedList<Dio32>();

            // check for defaults at 0xFF and 0x00
            byte eePromByte = 0xFF;
            var index = NativeMethods.GetDeviceByEEPROMByte(
                eePromByte);
            if (index < 0xFFFFFFFF)
            {
                dio32s.AddLast(
                    new CyUsbDio32(index, eePromByte));
            }

            eePromByte = 0;
            index = NativeMethods.GetDeviceByEEPROMByte(
                eePromByte);
            if (index < 0xFFFFFFFF)
            {
                dio32s.AddLast(
                    new CyUsbDio32(index, eePromByte));
            }
            
            for (eePromByte = 1; eePromByte < 0xFF; ++eePromByte)
            {
                index = NativeMethods.GetDeviceByEEPROMByte(
                    eePromByte);

                if (index == 0xFFFFFFFF)
                {
                    continue;
                }

                dio32s.AddLast(
                    new CyUsbDio32(index, eePromByte));
            }

            return new LinkedListMaterializedEnumerable<Dio32>(
                dio32s);
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
