namespace xofz.Framework.Erudition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Materialization;

    public class Darkener
    {
        public virtual MaterializedEnumerable<T> Darken<T>(
            IEnumerable<T> source, 
            BigInteger limiter)
        {
            var hashCodes = new LinkedList<int>(); // hash codes should be longs
            var darkenedCollection = new LinkedList<T>();

            foreach (var item in source)
            {
                var hashCode = item.GetHashCode();
                if (hashCodes.Any(hc => Math.Abs(hashCode - hc) < limiter))
                {
                    continue;
                }

                hashCodes.AddLast(hashCode);
                darkenedCollection.AddLast(item);
            }

            return new LinkedListMaterializedEnumerable<T>(
                darkenedCollection);
        }
    }
}
