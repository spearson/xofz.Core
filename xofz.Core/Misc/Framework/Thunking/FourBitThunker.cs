namespace xofz.Misc.Framework.Thunking
{
    using System;
    using System.Numerics;

    public sealed class FourBitThunker : Thunker
    {
        public bool[] Thunk(byte b)
        {
            var array = new bool[4];
            var first2Bits = b >> 6;
            array[0] = first2Bits == 0x3;

            var next2Bits = (b & 0x3F) >> 4;
            array[1] = next2Bits == 0x3;

            next2Bits = (b & 0x1F) >> 2;
            array[2] = next2Bits == 0x3;

            var last2Bits = b & 3;
            array[3] = last2Bits == 0x3;

            return array;
        }

        public bool[] Thunk(short s)
        {
            var array = new bool[4];
            var first4Bits = s >> 12;
            array[0] = first4Bits == 0xF;

            var next4Bits = (s & 0xFFF) >> 8;
            array[1] = next4Bits == 0xF;

            next4Bits = (s & 0xFF) >> 4;
            array[2] = next4Bits == 0xF;

            var last4Bits = s & 0xF;
            array[3] = last4Bits == 0xF;

            return array;
        }

        public bool[] Thunk(int i)
        {
            var array = new bool[4];
            var firstByte = i >> 24;
            array[0] = firstByte == 0xFF;

            var nextByte = (i & 0xFFFFFF) >> 16;
            array[1] = nextByte == 0xFF;

            nextByte = (i & 0xFFFF) >> 8;
            array[2] = nextByte == 0xFF;

            var lastByte = i & 0xFF;
            array[3] = lastByte == 0xFF;

            return array;
        }

        public bool[] Thunk(long l)
        {
            var array = new bool[4];
            var firstShort = l >> 48;
            array[0] = firstShort % 0xFFFF == 0;

            var nextShort = (l & 0xFFFFFFFFFFFF) >> 32;
            array[1] = nextShort % 0xFFFF == 0;

            nextShort = (l & 0xFFFFFFFF) >> 16;
            array[2] = nextShort % 0xFFFF == 0;

            var lastShort = l & 0xFFFF;
            array[3] = lastShort % 0xFFFF == 0;

            return array;
        }


        public bool[] Thunk(BigInteger bigNumber)
        {
            var array = new bool[4];
            var bits = bigNumber.ToByteArray();
            if (bits.Length == 1)
            {
                return this.Thunk(bits[0]);
            }

            if (bits.Length == 2)
            {
                return this.Thunk(BitConverter.ToInt16(bits, 0));
            }

            var quarterPoint = bits.Length / 4;
            var thunkChunk1 = true;
            for (var i = 0; i < quarterPoint; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk1);
                if (thunkChunk1 != true)
                {
                    break;
                }
            }

            var thunkChunk2 = true;
            for (var i = quarterPoint; i < quarterPoint * 2; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk2);
                if (thunkChunk2 != true)
                {
                    break;
                }
            }

            var thunkChunk3 = true;
            for (var i = quarterPoint * 2; i < quarterPoint * 3; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk3);
                if (thunkChunk3 != true)
                {
                    break;
                }
            }

            var thunkChunk4 = true;
            for (var i = quarterPoint * 3; i < bits.Length; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk4);
                if (thunkChunk4 != true)
                {
                    break;
                }
            }

            array[0] = thunkChunk1;
            array[1] = thunkChunk2;
            array[2] = thunkChunk3;
            array[3] = thunkChunk4;

            return array;
        }

        private void setThunkChunk(byte b, out bool thunkChunk)
        {
            thunkChunk = b % 0xFF == 0;
        }
    }
}
