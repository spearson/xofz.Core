namespace xofz.Framework.Theory
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class BinaryTranslator
    {
        public virtual IEnumerable<bool> GetBits(BigInteger number)
        {
            var array = number.ToByteArray();
            foreach (var b in array)
            {
                yield return this.getBit(b, 7);
                yield return this.getBit(b, 6);
                yield return this.getBit(b, 5);
                yield return this.getBit(b, 4);
                yield return this.getBit(b, 3);
                yield return this.getBit(b, 2);
                yield return this.getBit(b, 1);
                yield return this.getBit(b, 0);
            }
        }

        public virtual BigInteger GetNumber(IEnumerable<bool> bits)
        {
            var ll = new LinkedList<bool>(bits);
            var e = ll.GetEnumerator();
            var counter = 0;
            var array = new byte[(ll.Count + 7) / 8];
            while (true)
            {
                if (!e.MoveNext())
                {
                    break;
                }

                var bit1 = e.Current;
                byte currentByte;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit2 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit3 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit4 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit5 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit6 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5,
                            bit6
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit7 = e.Current;
                if (!e.MoveNext())
                {
                    currentByte = this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5,
                            bit6,
                            bit7
                        });
                    array[counter] = currentByte;
                    break;
                }

                var bit8 = e.Current;
                currentByte = this.getByte(
                    new[]
                    {
                        bit1,
                        bit2,
                        bit3,
                        bit4,
                        bit5,
                        bit6,
                        bit7,
                        bit8
                    });

                array[counter] = currentByte;
                ++counter;
            }

            return new BigInteger(array);
        }

        private bool getBit(byte b, byte shift)
        {
            return (b >> shift) % 2 == 1;
        }

        private byte getByte(bool[] bits)
        {
            byte result = 0;
            for (var i = 0; i < bits.Length; ++i)
            {
                result += bits[i] ? (byte)(1 << (7 - i)) : (byte)0;
            }

            return result;
        }
    }
}
