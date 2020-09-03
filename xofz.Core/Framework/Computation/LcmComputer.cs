namespace xofz.Framework.Computation
{
    using System;
    using System.Numerics;

    public class LcmComputer
    {
        [Obsolete(@"This class does not actually find the least common multiple; it finds some multiple of that.")]
        public LcmComputer(
            FactorialComputer factorialComputer)
        {
            this.factorialComputer = factorialComputer;
        }

        public virtual BigInteger Compute(
            short range)
        {
            if (range < 1)
            {
                return 0;
            }

            if (range < 2)
            {
                return 1;
            }

            var total = this.factorialComputer.Compute(range);
            var current = total;
            var counter = 2;
            while (counter <= range)
            {
                current = current / counter;
                for (var i = 2; i <= range; ++i)
                {
                    if (current % i != 0)
                    {
                        // backtrack
                        current = current * counter;
                        break;
                    }
                }

                ++counter;
            }

            return current;
        }

        protected readonly FactorialComputer factorialComputer;
    }
}
