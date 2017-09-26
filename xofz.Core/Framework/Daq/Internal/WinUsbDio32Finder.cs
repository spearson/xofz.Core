namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using xofz.Framework.Materialization;

    internal sealed class WinUsbDio32Finder
    {
        public WinUsbDio32Finder()
        {
            this.locker = new object();
        }

        public MaterializedEnumerable<Dio32> Find()
        {
            return this.findInternal(
                dio32 => dio32.ReadSerialNumber());
        }

        public MaterializedEnumerable<Dio32> FindWithOutputs(
            Dio32Ports outputs)
        {
            return this.findInternal(
                dio32 => dio32.Configure(
                    dio32.ReadOnTerminals(), outputs));
        }

        private MaterializedEnumerable<Dio32> findInternal(
            Action<Dio32> tester)
        {
            var dps = this.findDevicePaths();
            var dio32s = new LinkedList<Dio32>();
            foreach (var dio32 in dps.Select(
                dp => new WinUsbDio32(dp)))
            {
                try
                {
                    tester(dio32);
                }
                catch (IOException)
                {
                    continue;
                }

                dio32s.AddLast(dio32);
            }

            return new LinkedListMaterializedEnumerable<Dio32>(
                dio32s);
        }

        private MaterializedEnumerable<string> findDevicePaths()
        {
            var paths = new LinkedList<string>();
            var deviceClassId = Guid.Parse("88d87f5a-ea16-93fc-2165-39c91c36c96e");
            lock (this.locker)
            {
                using (var handle = NativeMethods.SetupDiGetClassDevs(
                    ref deviceClassId,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    DeviceFilters.Present | DeviceFilters.HasDeviceInterface))
                {
                    uint memberIndex = 0;
                    var data = new DeviceInterfaceData();
                    data.Size = (uint)Marshal.SizeOf(data);
                    while (NativeMethods.SetupDiEnumDeviceInterfaces(
                        handle,
                        IntPtr.Zero,
                        ref deviceClassId,
                        memberIndex,
                        ref data))
                    {
                        ++memberIndex;
                        int requiredSize;
                        NativeMethods.SetupDiGetDeviceInterfaceDetail(
                            handle,
                            ref data,
                            IntPtr.Zero,
                            0,
                            out requiredSize,
                            IntPtr.Zero);

                        var size = IntPtr.Size == 8
                            ? 8
                            : 4 + Marshal.SystemDefaultCharSize;

                        var details = new DeviceInterfaceDetailData
                        {
                            Size = size
                        };

                        NativeMethods.SetupDiGetDeviceInterfaceDetail(
                            handle,
                            ref data,
                            ref details,
                            requiredSize,
                            out requiredSize,
                            IntPtr.Zero);

                        paths.AddLast(details.DevicePath);
                    }
                }
            }

            return new LinkedListMaterializedEnumerable<string>(paths);
        }

        private readonly object locker;
    }
}
