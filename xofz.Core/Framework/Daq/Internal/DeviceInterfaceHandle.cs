namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class DeviceInterfaceHandle : SafeHandle
    {
        public DeviceInterfaceHandle()
            : base(IntPtr.Zero, true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.WinUsb_Free(this.handle);
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }
}
