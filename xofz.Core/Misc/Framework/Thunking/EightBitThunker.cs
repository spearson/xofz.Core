namespace xofz.Misc.Framework.Thunking
{
    using System;
    using System.Numerics;

    public sealed class EightBitThunker : Thunker
    {
        public bool[] Thunk(byte b)
        {
            var array = new bool[8];
            var firstBit = b >> 7;
            array[0] = firstBit == 0x1;

            var secondBit = (b >> 6) & 0x1;
            array[1] = secondBit == 0x1;

            var thirdBit = (b >> 5) & 0x1;
            array[2] = thirdBit == 0x1;

            var fourthBit = (b >> 4) & 0x1;
            array[3] = fourthBit == 0x1;

            var fifthBit = (b >> 3) & 0x1;
            array[4] = fifthBit == 0x1;

            var sixthBit = (b >> 2) & 0x1;
            array[5] = sixthBit == 0x1;

            var seventhBit = (b >> 1) & 0x1;
            array[6] = seventhBit == 0x1;

            var eighthBit = b & 0x1;
            array[7] = eighthBit == 0x1;

            return array;
        }

        public bool[] Thunk(short s)
        {
            var array = new bool[8];
            var first2Bits = s >> 14;
            array[0] = first2Bits == 0x3;

            var next2Bits = (s >> 12) & 0x3;
            array[1] = next2Bits == 0x3;

            next2Bits = (s >> 10) & 0x3;
            array[2] = next2Bits == 0x3;

            next2Bits = (s >> 8) & 0x3;
            array[3] = next2Bits == 0x3;

            next2Bits = (s >> 6) & 0x3;
            array[4] = next2Bits == 0x3;

            next2Bits = (s >> 4) & 0x3;
            array[5] = next2Bits == 0x3;

            next2Bits = (s >> 2) & 0x3;
            array[6] = next2Bits == 0x3;

            var last2Bits = s & 0x3;
            array[7] = last2Bits == 0x3;

            return array;
        }

        public bool[] Thunk(int i)
        {
            var array = new bool[8];
            var first4Bits = (i >> 28) & 0xF;
            array[0] = first4Bits == 0xF;

            var next4Bits = (i >> 24) & 0xF;
            array[1] = next4Bits == 0xF;

            next4Bits = (i >> 20) & 0xF;
            array[2] = next4Bits == 0xF;

            next4Bits = (i >> 16) & 0xF;
            array[3] = next4Bits == 0xF;

            next4Bits = (i >> 12) & 0xF;
            array[4] = next4Bits == 0xF;

            next4Bits = (i >> 8) & 0xF;
            array[5] = next4Bits == 0xF;

            next4Bits = (i >> 4) & 0xF;
            array[6] = next4Bits == 0xF;

            var last4Bits = i & 0xF;
            array[7] = last4Bits == 0xF;

            return array;
        }

        public bool[] Thunk(long l)
        {
            var array = new bool[8];
            var firstByte = (l >> 56) & 0xFF;
            array[0] = firstByte == 0xFF;

            var nextByte = (l >> 48) & 0xFF;
            array[1] = nextByte == 0xFF;

            nextByte = (l >> 40) & 0xFF;
            array[2] = nextByte == 0xFF;

            nextByte = (l >> 32) & 0xFF;
            array[3] = nextByte == 0xFF;

            nextByte = (l >> 24) & 0xFF;
            array[4] = nextByte == 0xFF;

            nextByte = (l >> 16) & 0xFF;
            array[5] = nextByte == 0xFF;

            nextByte = (l >> 8) & 0xFF;
            array[6] = nextByte == 0xFF;

            var lastByte = l & 0xFF;
            array[7] = lastByte == 0xFF;

            return array;
        }

        public bool[] Thunk(BigInteger bigNumber)
        {
            var array = new bool[8];
            var bits = bigNumber.ToByteArray();

            if (bits.Length == 1)
            {
                return this.Thunk(bits[0]);
            }

            if (bits.Length == 2)
            {
                return this.Thunk(BitConverter.ToInt16(bits, 0));
            }

            if (bits.Length == 4)
            {
                return this.Thunk(BitConverter.ToInt32(bits, 0));
            }

            if (bits.Length == 8)
            {
                return this.Thunk(BitConverter.ToInt64(bits, 0));
            }

            var eighthPoint = bits.Length / 8;
            var thunkChunk1 = true;
            for (var i = 0; i < eighthPoint; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk1);
                if (thunkChunk1 != true)
                {
                    break;
                }
            }

            var thunkChunk2 = true;
            for (var i = eighthPoint; i < eighthPoint * 2; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk2);
                if (thunkChunk2 != true)
                {
                    break;
                }
            }

            var thunkChunk3 = true;
            for (var i = eighthPoint * 2; i < eighthPoint * 3; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk3);
                if (thunkChunk3 != true)
                {
                    break;
                }
            }

            var thunkChunk4 = true;
            for (var i = eighthPoint * 3; i < eighthPoint * 4; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk4);
                if (thunkChunk4 != true)
                {
                    break;
                }
            }

            var thunkChunk5 = true;
            for (var i = eighthPoint * 4; i < eighthPoint * 5; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk5);
                if (thunkChunk5 != true)
                {
                    break;
                }
            }

            var thunkChunk6 = true;
            for (var i = eighthPoint * 5; i < eighthPoint * 6; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk6);
                if (thunkChunk6 != true)
                {
                    break;
                }
            }

            var thunkChunk7 = true;
            for (var i = eighthPoint * 6; i < eighthPoint * 7; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk7);
                if (thunkChunk7 != true)
                {
                    break;
                }
            }

            var thunkChunk8 = true;
            for (var i = eighthPoint * 7; i < bits.Length; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk8);
                if (thunkChunk8 != true)
                {
                    break;
                }
            }

            array[0] = thunkChunk1;
            array[1] = thunkChunk2;
            array[2] = thunkChunk3;
            array[3] = thunkChunk4;
            array[4] = thunkChunk5;
            array[5] = thunkChunk6;
            array[6] = thunkChunk7;
            array[7] = thunkChunk8;

            return array;
        }

        private void setThunkChunk(byte b, out bool thunkChunk)
        {
            thunkChunk = b % 0xFF == 0;
        }
    }
}
