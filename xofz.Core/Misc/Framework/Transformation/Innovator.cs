namespace xofz.Misc.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using static EnumerableHelpers;

    public class Innovator
    {
        public virtual IEnumerable<T> Innovate<T>(
            Lot<T>[] lots,
            Func<T, BigInteger> valueChooser)
        {
            if (lots == null || lots.Length < 1)
            {
                yield break;
            }

            var enumerators = new LinkedList<IEnumerator<T>>();
            foreach (var lot in lots)
            {
                if (lot == null)
                {
                    continue;
                }

                enumerators.AddLast(lot.GetEnumerator());
            }

            while (enumerators.First.Value.MoveNext())
            {
                BigInteger leastValue = long.MaxValue;
                BigInteger maximalValue = 0;
                var leastItem = default(T);
                var maximalItem = default(T);
                foreach (var enumerator in Skip(
                    enumerators, 1))
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
