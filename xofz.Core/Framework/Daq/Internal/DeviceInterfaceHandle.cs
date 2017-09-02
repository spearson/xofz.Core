// ----------------------------------------------------------------------------
// <copyright file="DeviceInterfaceHandle.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

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
