namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using System.Numerics;

    public class MultiPow
    {
        public MultiPow(
            BigPow bigPow)
        {
            this.bigPow = bigPow;
        }

        public virtual BigInteger Compute(
            params BigInteger[] powers)
        {
            if (powers == null)
            {
                return zero;
            }

            return this.onCompute(powers);
        }

        public virtual BigInteger Compute(
            IEnumerable<BigInteger> powers)
        {
            if (powers == null)
            {
                return zero;
            }

            return this.onCompute(
                new List<BigInteger>(powers));
        }

        protected virtual BigInteger onCompute(
            IList<BigInteger> powers)
        {
            if (powers == null)
            {
                return zero;
            }

            var pc = powers.Count;
            if (pc < one)
            {
                return zero;
            }

            if (pc == one)
            {
                return powers[zero];
            }

            var bp = this.bigPow;
            if (bp == null)
            {
                return zero;
            }

            BigInteger result = 1;
            for (int i = powers.Count - one; i > zero; --i)
            {
                result = bp.Compute(
                    powers[i - one], 
                    powers[i]);
                powers[i - one] = result;
            }

            return result;
        }

        protected readonly BigPow bigPow;
        protected const byte 
            zero = 0, 
            one = 1;
    }
}
