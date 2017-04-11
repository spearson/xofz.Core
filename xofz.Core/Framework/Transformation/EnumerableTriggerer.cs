namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class EnumerableTriggerer
    {
        public IEnumerable<T> Trigger<T>(
            IEnumerable<T> source,
            MaterializedEnumerable<BigInteger> triggerPoints,
            Action<T> trigger)
        {
            BigInteger triggerCounter = 0;
            foreach (var item in source)
            {
                ++triggerCounter;
                if (triggerPoints.Contains(triggerCounter))
                {
                    trigger(item);
                }

                yield return item;
            }
        }
    }
}
