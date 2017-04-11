namespace xofz.Framework.Theory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BinaryTranslator
    {
        public virtual IEnumerable<bool> GetBits(BigInteger number)
        {
            return this.GetBits(number.ToByteArray());
        }

        public virtual IEnumerable<bool> GetBits(string s, Encoding encoding)
        {
            return this.GetBits(encoding.GetBytes(s));
        }

        public virtual IEnumerable<bool> GetBits(IEnumerable<byte> bytes)
        {
            foreach (var b in bytes)
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

        public virtual BigInteger ReadNumber(IEnumerable<bool> bits)
        {
            return new BigInteger(this.GetBytes(bits).ToArray());
        }

        public virtual string ReadString(IEnumerable<bool> bits, Encoding encoding)
        {
            return encoding.GetString(this.GetBytes(bits).ToArray());
        }

        public virtual IEnumerable<byte> GetBytes(IEnumerable<bool> bits)
        {
            var ll = new LinkedList<bool>(bits);
            var e = ll.GetEnumerator();
            while (true)
            {
                if (!e.MoveNext())
                {
                    e.Dispose();
                    break;
                }

                var bit1 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1
                        });
                }

                var bit2 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2
                        });
                }

                var bit3 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3
                        });
                }

                var bit4 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4
                        });
                }

                var bit5 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5
                        });
                }

                var bit6 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5,
                            bit6
                        });
                }

                var bit7 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
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
                }

                var bit8 = e.Current;
                yield return this.getByte(
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
            }
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
