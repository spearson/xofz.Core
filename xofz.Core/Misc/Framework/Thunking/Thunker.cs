namespace xofz.Misc.Framework.Thunking
{
    using System.Numerics;

    public interface Thunker
    {
        bool[] Thunk(byte b);

        bool[] Thunk(short s);

        bool[] Thunk(int i);

        bool[] Thunk(long l);

        bool[] Thunk(BigInteger bigNumber);
    }
}
