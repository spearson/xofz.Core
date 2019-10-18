namespace xofz.Misc.Framework.Thunking
{
    using System;
    using System.Numerics;

    public sealed class TwoBitThunker 
        : Thunker
    {
        public TwoBitThunker()
        {
            this.maxValue = int.MaxValue;
        }

        public bool[] Thunk(
            byte b)
        {
            var array = new bool[2];

            var firstFourBits = b >> 4;
            array[0] = firstFourBits % 15 == 0;

            var lastFourBits = b & 15;
            array[1] = lastFourBits % 15 == 0;

            return array;
        }

        public bool[] Thunk(
            short s)
        {
            var array = new bool[2];

            var firstByte = s >> 8;
            array[0] = firstByte % 0xFF == 0;

            var lastByte = s & 255;
            array[1] = lastByte % 0xFF == 0;

            return array;
        }

        public bool[] Thunk(
            int i)
        {
            var array = new bool[2];

            var firstShort = i >> 16;
            array[0] = firstShort % 0xFFFF == 0;

            var lastShort = i & 32767;
            array[1] = lastShort % 0xFFFF == 0;

            return array;
        }

        public bool[] Thunk(
            long l)
        {
            var array = new bool[2];
            var max = this.maxValue;

            var firstInt = l >> 32;
            array[0] = firstInt % max == 0;

            var lastInt = l & int.MaxValue;
            array[1] = lastInt % max == 0;

            return array;
        }

        public bool[] Thunk(
            BigInteger bigNumber)
        {
            var array = new bool[2];
            var bits = bigNumber.ToByteArray();
            if (bits.Length == 1)
            {
                return this.Thunk(bits[0]);
            }

            if (bits.Length == 2)
            {
                return this.Thunk(BitConverter.ToInt16(bits, 0));
            }

            var midpoint = (bits.Length / 2) + 1;
            var thunkChunk1 = true;
            for (var i = 0; i < midpoint; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk1);
                if (thunkChunk1 != true)
                {
                    break;
                }
            }

            var thunkChunk2 = true;
            for (var i = midpoint; i < bits.Length; ++i)
            {
                this.setThunkChunk(bits[i], out thunkChunk2);
                if (thunkChunk2 != true)
                {
                    break;
                }
            }

            array[0] = thunkChunk1;
            array[1] = thunkChunk2;

            return array;
        }

        private void setThunkChunk(
            byte b, 
            out bool thunkChunk)
        {
            thunkChunk = b % 0xFF == 0;
        }

        private readonly int maxValue;
    }
}
