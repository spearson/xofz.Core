namespace xofz.Framework.Daq.Internal
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    internal struct DeviceInterfaceDetailData
    {
        public int Size;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public readonly string DevicePath;
    }
}
