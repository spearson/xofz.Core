// ----------------------------------------------------------------------------
// <copyright file="DeviceInterfaceData.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

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
