// ----------------------------------------------------------------------------
// <copyright file="DeviceFilters.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace xofz.Framework.Daq.Internal
{
    using System;

    [Flags]
    internal enum DeviceFilters : uint
    {
        Present = 0x2,
        HasDeviceInterface = 0x10
    }
}
