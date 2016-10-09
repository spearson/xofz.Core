namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableIntersector
    {
        public virtual MaterializedEnumerable<T> Intersect<T>(IEnumerable<IEnumerable<T>> sources)
        {
            var hashSet = new HashSet<T>();
            foreach (var source in sources)
            {
                foreach (var item in source)
                {
                    hashSet.Add(item);
                }
            }

            return new HashSetMaterializedEnumerable<T>(hashSet);
        }
    }
}
