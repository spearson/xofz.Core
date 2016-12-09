namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;

    public class EnumerableInjector
    {
        public virtual IEnumerable<T> Inject<T>(
            IEnumerable<T> source,
            MaterializedEnumerable<int> injectionPoints,
            params T[] injections)
        {
            var counter = 0;
            var index = 0;
            foreach (var item in source)
            {
                ++counter;
                if (injectionPoints.Contains(counter))
                {
                    yield return injections[index];
                    ++index;
                }

                yield return item;
            }
        }
    }
}
