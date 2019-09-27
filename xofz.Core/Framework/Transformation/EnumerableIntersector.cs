namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableIntersector
    {
        public virtual Lot<T> Intersect<T>(
            IEnumerable<IEnumerable<T>> sources)
        {
            var hashSet = new HashSetLot<T>();
            if (sources == null)
            {
                return hashSet;
            }
            
            foreach (var source in sources)
            {
                if (source == null)
                {
                    continue;
                }

                foreach (var item in source)
                {
                    hashSet.Add(item);
                }
            }

            return hashSet;
        }
    }
}
