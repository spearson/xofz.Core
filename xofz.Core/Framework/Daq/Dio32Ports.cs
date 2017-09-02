// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Dio32Ports.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace xofz.Framework.Daq
{
    using System;

    [Flags]
    public enum Dio32Ports : byte
    {
        None = 0x0,

        A = 0x1,
        B = 0x2,
        C = 0x4,
        D = 0x8,

        All = A | B | C | D
    }
}
