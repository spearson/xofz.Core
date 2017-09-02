namespace xofz.Framework.Daq.Internal
{
    using System;

    [Flags]
    internal enum FileAttributes : uint
    {
        Overlapped = 0x40000000
    }
}
