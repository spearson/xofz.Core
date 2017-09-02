namespace xofz.Framework.Daq
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using xofz.Framework.Daq.Internal;

    public sealed class WinUsbDio32Finder : Dio32Finder
    {
        IList<Dio32> Dio32Finder.Find()
        {
            var devicePaths = this.findDevicePaths();
            var dio32s = new List<Dio32>(devicePaths.Count);
            foreach (var dio32 in devicePaths.Select(path => (Dio32)new WinUsbDio32(path)))
            {
                try
                {
                    dio32.Configure(dio32.ReadOnTerminals(), Dio32Ports.A | Dio32Ports.B | Dio32Ports.C);
                }
                catch (IOException)
                {
                    continue;
                }

                dio32s.Add(dio32);
            }

            return dio32s;
        }

        private List<string> findDevicePaths()
        {
            var paths = new List<string>();
            var deviceClassId = Guid.Parse("88d87f5a-ea16-93fc-2165-39c91c36c96e");
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

                    paths.Add(details.DevicePath);
                }
            }

            return paths;
        }
    }
}
