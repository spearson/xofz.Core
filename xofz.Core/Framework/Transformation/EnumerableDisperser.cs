namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;

    public class EnumerableDisperser
    {
        public virtual IEnumerable<T> Disperse<T>(
            IEnumerable<T> source,
            IEnumerable<T> dispersion,
            MaterializedEnumerable<int> dispersionPoints)
        {
            var e = dispersion.GetEnumerator();
            var counter = 0;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                e.MoveNext();
                if (dispersionPoints.Contains(counter))
                {
                    yield return e.Current;
                }
            }

            e.Dispose();
        }
    }
}
