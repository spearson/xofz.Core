namespace xofz.Framework.Daq.Internal
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct SetupPacket
    {
        public byte RequestType;
        public byte Request;
        public ushort Value;
        public ushort Index;
        public ushort Length;
    }
}
