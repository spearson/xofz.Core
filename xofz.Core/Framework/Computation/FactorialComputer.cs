namespace xofz.Framework.Computation
{
    using System.Numerics;

    public class FactorialComputer
    {
        public virtual BigInteger Compute(
            BigInteger number)
        {
            if (number <= one)
            {
                return one;
            }

            if (number == two)
            {
                return two;
            }

            BigInteger counter = number;
            BigInteger powersOf2 = one;
            while (counter > zero)
            {
                if (this.numberIsPowerOfTwo(counter))
                {
                    var powerOf2 = this.computePowerOf2(
                        counter);
                    powersOf2 <<= powerOf2;
                    counter >>= one;
                }
                else
                {
                    --counter;
                }
            }

            // all other numbers here
            BigInteger nonPowersOf2 = one;
            for (var i = number; i > two; --i)
            {
                // test for power of 2
                if (this.numberIsPowerOfTwo(i))
                {
                    continue;
                }

                nonPowersOf2 *= i;
            }

            return powersOf2 * nonPowersOf2;
        }

        protected virtual bool numberIsPowerOfTwo(
            BigInteger n)
        {
            // stolen from stackoverflow -- thank you!!
            return (n & (n - one)) == 0;
        }

        protected virtual int computePowerOf2(
            BigInteger numberRaisedToPowerOf2)
        {
            int powerOf2 = 0;
            while (true)
            {
                if (numberRaisedToPowerOf2 < two)
                {
                    return powerOf2;
                }

                ++powerOf2;
                numberRaisedToPowerOf2 >>= one;
            }
        }

        protected const byte zero = 0, one = 1, two = 2;
    }
}
