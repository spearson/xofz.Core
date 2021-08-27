namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using xofz.Framework.Lots;
    using static EnumerableHelpers;

    public class Darkener
    {
        public virtual Lot<T> Darken<T>(
            IEnumerable<T> source, 
            BigInteger limiter)
        {
            var hashCodes = new XLinkedList<int>(); // hash codes should be longs
            var darkenedCollection = new XLinkedList<T>();

            foreach (var item in source)
            {
                var hashCode = item.GetHashCode();
                if (Any(
                    hashCodes,
                    hc => Math.Abs(hashCode - hc) < limiter))
                {
                    continue;
                }

                hashCodes.AddTail(hashCode);
                darkenedCollection.AddTail(item);
            }

            return new LinkedListLot<T>(
                darkenedCollection);
        }
    }
}
