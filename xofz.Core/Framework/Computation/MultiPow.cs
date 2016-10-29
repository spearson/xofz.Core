namespace xofz.Framework.Computation
{
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

        private readonly BigPow bigPow;
    }
}
