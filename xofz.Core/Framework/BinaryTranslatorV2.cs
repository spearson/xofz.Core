namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Numerics;
    using static EnumerableHelpers;

    public class BinaryTranslatorV2
        : BinaryTranslator
    {
        public virtual BigInteger ReadBigInteger(
            IEnumerable<bool> bits)
        {
            var bytes = this.GetBytes(bits);
            return new BigInteger(ToArray(bytes) ?? new byte[0]);
        }

        public virtual IEnumerable<bool> GetBits(
            BigInteger number)
        {
            return this.GetBits(
                number.ToByteArray());
        }
    }
}
