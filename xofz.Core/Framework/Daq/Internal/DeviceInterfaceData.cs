namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct DeviceInterfaceData
    {
        public uint Size;
        private readonly Guid interfaceClassGuid;
        private readonly uint flags;
        private readonly IntPtr reserved;
    }
}
