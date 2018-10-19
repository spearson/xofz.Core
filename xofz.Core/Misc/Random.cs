namespace xofz.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Random<T>
    {
        public Random(Lot<T> lot)
        {
            this.lot = lot;
            this.random = new Random();
        }

        public virtual T Next(BigInteger maxToPass)
        {
            // i'll do something better than a shave to an int later
            return this.lot.Skip(this.random.Next((int)maxToPass)).FirstOrDefault();
        }

        public virtual IEnumerable<T> NextSequence(BigInteger maxToPass, BigInteger sourceSize)
        {
            var c = this.lot;
            BigInteger counter = 0;
            while (counter < sourceSize)
            {
                var nextSkip = this.random.Next((int)maxToPass);
                ++counter;
                yield return c.Skip(nextSkip).FirstOrDefault();
            }
        }

        private readonly Lot<T> lot;
        private readonly Random random;
    }
}
