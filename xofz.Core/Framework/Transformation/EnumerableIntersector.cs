namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableIntersector
    {
        public virtual Lot<T> Intersect<T>(IEnumerable<IEnumerable<T>> sources)
        {
            var hashSet = new HashSet<T>();
            foreach (var source in sources)
            {
                foreach (var item in source)
                {
                    hashSet.Add(item);
                }
            }

            return new HashSetLot<T>(hashSet);
        }
    }
}
