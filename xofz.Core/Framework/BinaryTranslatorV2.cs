namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Numerics;
    using EH = xofz.EnumerableHelpers;

    public class BinaryTranslatorV2
        : BinaryTranslator
    {
        public virtual BigInteger ReadBigInteger(
            IEnumerable<bool> bits)
        {
            var bytes = this.GetBytes(bits);
            const byte zero = 0;
            return new BigInteger(
                EH.ToArray(bytes) ?? 
                new byte[zero]);
        }

        public virtual IEnumerable<bool> GetBits(
            BigInteger number)
        {
            return this.GetBits(
                number.ToByteArray());
        }
    }
}
