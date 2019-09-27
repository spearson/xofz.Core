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
                return 0;
            }

            return this.onCompute(powers);
        }

        public virtual BigInteger Compute(
            IEnumerable<BigInteger> powers)
        {
            if (powers == null)
            {
                return 0;
            }

            return this.onCompute(
                new List<BigInteger>(powers));
        }

        protected virtual BigInteger onCompute(
            IList<BigInteger> powers)
        {
            if (powers == null)
            {
                return 0;
            }

            var bp = this.bigPow;
            BigInteger result = 1;
            for (var i = powers.Count - 1; i > 0; --i)
            {
                result = bp.Compute(powers[i - 1], powers[i]);
                powers[i - 1] = result;
            }

            return result;
        }

        protected readonly BigPow bigPow;
    }
}
