// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dio32Terminals.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace xofz.Framework.Daq
{
    using System;

    [Flags]
    public enum Dio32Terminals : long
    {
        None = 0x0,

        A0 = 0x1,
        A1 = 0x2,
        A2 = 0x4,
        A3 = 0x8,
        A4 = 0x10,
        A5 = 0x20,
        A6 = 0x40,
        A7 = 0x80,

        B0 = 0x100,
        B1 = 0x200,
        B2 = 0x400,
        B3 = 0x800,
        B4 = 0x1000,
        B5 = 0x2000,
        B6 = 0x4000,
        B7 = 0x8000,

        C0 = 0x10000,
        C1 = 0x20000,
        C2 = 0x40000,
        C3 = 0x80000,
        C4 = 0x100000,
        C5 = 0x200000,
        C6 = 0x400000,
        C7 = 0x800000,

        D0 = 0x1000000,
        D1 = 0x2000000,
        D2 = 0x4000000,
        D3 = 0x8000000,
        D4 = 0x10000000,
        D5 = 0x20000000,
        D6 = 0x40000000,
        D7 = 0x80000000,

        PortA = A0 | A1 | A2 | A3 | A4 | A5 | A6 | A7,
        PortB = B0 | B1 | B2 | B3 | B4 | B5 | B6 | B7,
        PortC = C0 | C1 | C2 | C3 | C4 | C5 | C6 | C7,
        PortD = D0 | D1 | D2 | D3 | D4 | D5 | D6 | D7,

        All = PortA | PortB | PortC | PortD
    }
}
