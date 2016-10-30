namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class MultiPow
    {
        public MultiPow(BigPow bigPow)
        {
            this.bigPow = bigPow;
        }

        public virtual BigInteger Compute(params BigInteger[] powers)
        {
            var bp = this.bigPow;
            BigInteger result = 1;
            for (var i = powers.Length - 1; i > 0; --i)
            {
                result = bp.Compute(powers[i - 1], powers[i]);
                powers[i - 1] = result;
            }

            return result;
        }

        public virtual BigInteger Compute(IEnumerable<BigInteger> powers)
        {
            var bp = this.bigPow;
            var l = new List<BigInteger>(powers);
            BigInteger result = 1;
            for (var i = l.Count - 1; i > 0; --i)
            {
                result = bp.Compute(l[i - 1], l[i]);
                l[i - 1] = result;
            }

            return result;
        }

        private readonly BigPow bigPow;
    }
}
