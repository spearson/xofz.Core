namespace xofz.Framework.Computation
{
    using System.Numerics;

    public class BigPow
    {
        public virtual BigInteger Compute(
            BigInteger n, 
            BigInteger exponent)
        {
            const byte
                zero = 0,
                one = 1;
            BigInteger result = one;
            for (BigInteger i = zero; i < exponent; ++i)
            {
                result *= n;
            }

            return result;
        }
    }
}
