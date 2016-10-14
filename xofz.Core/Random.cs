namespace xofz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Random<T>
    {
        public Random(MaterializedEnumerable<T> collection)
        {
            this.collection = collection;
            this.random = new Random();
        }

        public virtual T Next(BigInteger maxToPass)
        {
            // i'll do something better than a shave to an int later
            return this.collection.Skip(this.random.Next((int)maxToPass)).FirstOrDefault();
        }

        public virtual IEnumerable<T> NextSequence(BigInteger maxToPass, BigInteger sourceSize)
        {
            var c = this.collection;
            BigInteger counter = 0;
            while (counter < sourceSize)
            {
                var nextSkip = this.random.Next((int)maxToPass);
                ++counter;
                yield return c.Skip(nextSkip).FirstOrDefault();
            }
        }

        private readonly MaterializedEnumerable<T> collection;
        private readonly Random random;
    }
}
