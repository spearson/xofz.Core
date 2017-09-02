// ----------------------------------------------------------------------------
// <copyright file="FileAttributes.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace xofz.Framework.Daq.Internal
{
    using System;

    [Flags]
    internal enum FileAttributes : uint
    {
        Overlapped = 0x40000000
    }
}
