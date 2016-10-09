namespace xofz
{
    using System;
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

        private readonly MaterializedEnumerable<T> collection;
        private readonly Random random;
    }
}
