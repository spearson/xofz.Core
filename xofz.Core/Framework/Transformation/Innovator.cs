namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Innovator
    {
        public virtual IEnumerable<T> Innovate<T>(
            MaterializedEnumerable<T>[] collections,
            Func<T, BigInteger> valueChooser)
        {
            var enumerators = new LinkedList<IEnumerator<T>>();
            for (var i = 0; i < collections.Length; ++i)
            {
                enumerators.AddLast(collections[i].GetEnumerator());
            }

            while (enumerators.First.Value.MoveNext())
            {
                BigInteger leastValue = long.MaxValue;
                BigInteger maximalValue = 0;
                var leastItem = default(T);
                var maximalItem = default(T);
                foreach (var enumerator in enumerators.Skip(1))
                {
                    enumerator.MoveNext();
                    var value = valueChooser(enumerator.Current);
                    if (value < leastValue)
                    {
                        leastValue = value;
                        leastItem = enumerator.Current;
                    }

                    if (value > maximalValue)
                    {
                        maximalValue = value;
                        maximalItem = enumerator.Current;
                    }
                }

                yield return leastItem;
                yield return maximalItem;
            }
        }
    }
}
