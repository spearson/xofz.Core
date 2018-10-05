namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class BinaryTranslatorV2
        : BinaryTranslator
    {
        public virtual BigInteger ReadBigInteger(
            IEnumerable<bool> bits)
        {
            var bytes = this.GetBytes(bits);
            return new BigInteger(bytes?.ToArray() ?? new byte[0]);
        }

        public virtual IEnumerable<bool> GetBits(
            BigInteger number)
        {
            return this.GetBits(
                number.ToByteArray());
        }
    }
}
