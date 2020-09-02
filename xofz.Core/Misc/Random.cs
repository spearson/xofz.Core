namespace xofz.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using static EnumerableHelpers;

    public class Random<T>
    {
        public Random(
            Lot<T> lot)
            : this(lot, new Random())
        {
        }

        public Random(
            Lot<T> lot,
            Random random)
        {
            this.lot = lot ?? Lot.Empty<T>();
            this.random = random ?? new Random();
        }

        public virtual T Next(
            BigInteger maxToPass)
        {
            // i'll do something better than a shave to an int later
            return FirstOrDefault(
                Skip(
                    this.lot,
                    this.random.Next((int)maxToPass)));
        }

        public virtual IEnumerable<T> NextSequence(
            BigInteger maxToPass,
            BigInteger sourceSize)
        {
            var c = this.lot;
            BigInteger counter = 0;
            while (counter < sourceSize)
            {
                var nextSkip = this.random.Next((int)maxToPass);
                ++counter;
                yield return FirstOrDefault(
                    Skip(
                        c,
                        nextSkip));
            }
        }

        protected readonly Lot<T> lot;
        protected readonly Random random;
    }
}
