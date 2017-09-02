// ----------------------------------------------------------------------------
// <copyright file="DevInfoSetHandle.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class DevInfoSetHandle : SafeHandle
    {
        public DevInfoSetHandle()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.SetupDiDestroyDeviceInfoList(this.handle);
        }
    }
}
