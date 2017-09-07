namespace xofz.Framework.Computation
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Numerics;

    public class FactorialComputer
    {
        public virtual BigInteger Compute(BigInteger number)
        {
            if (number <= 1)
            {
                return 1;
            }

            if (number == 2)
            {
                return 2;
            }

            BigInteger counter = number;
            BigInteger powersOf2 = 1;
            while (counter > 0)
            {
                if (this.numberIsPowerOfTwo(counter))
                {
                    var powerOf2 = this.computePowerOf2(counter);
                    powersOf2 <<= powerOf2;
                    counter >>= 1;
                }
                else
                {
                    --counter;
                }
            }

            // all other numbers here
            BigInteger nonPowersOf2 = 1;
            for (var i = number; i > 2; --i)
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

        private bool numberIsPowerOfTwo(BigInteger n)
        {
            // stolen from stackoverflow -- thank you!!
            return (n & (n - 1)) == 0;
        }

        private int computePowerOf2(BigInteger numberRaisedToPowerOf2)
        {
            var powerOf2 = 0;
            while (true)
            {
                if (numberRaisedToPowerOf2 < 2)
                {
                    return powerOf2;
                }

                ++powerOf2;
                numberRaisedToPowerOf2 >>= 1;
            }
        }
    }

    public class TestFactorialBig
    {
        public void Go()
        {
            var computer = new FactorialComputer();
            BigInteger factorial;
            Stopwatch sw;
            sw = Stopwatch.StartNew();
            factorial = computer.Compute(100000);
            sw.Stop();
            Console.WriteLine(
                @"Computing the factorial of 100,000 took "
                + sw.Elapsed);

            Console.WriteLine(factorial);
        }

        public void GoBig()
        {
            var computer = new FactorialComputer();
            BigInteger factorial;
            const long bigNumber = 1000000;
            Stopwatch sw;
            sw = Stopwatch.StartNew();
            factorial = computer.Compute(bigNumber);
            sw.Stop();

            Console.WriteLine("Computing the factorial of " + bigNumber + " took " + sw.Elapsed);
            File.WriteAllText("Factorial of " + bigNumber + ".txt", factorial.ToString());
        }
    }
}
