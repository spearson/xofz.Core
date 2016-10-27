namespace xofz.Framework.Computation
{
    using System.Numerics;

    public class BigPow
    {
        public virtual BigInteger Compute(BigInteger n, BigInteger exponent)
        {
            BigInteger result = 1;
            for (BigInteger i = 0; i < exponent; ++i)
            {
                result *= n;
            }

            return result;
        }
    }
}
