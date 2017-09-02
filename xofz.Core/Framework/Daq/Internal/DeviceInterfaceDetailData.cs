// ----------------------------------------------------------------------------
// <copyright file="DeviceInterfaceDetailData.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

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
