namespace xofz.Framework.Materialization
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Internal;

    public class Materializer
    {
        // note: when overriding this method, do not ever return a LazyMaterializedEnumerable!
        public virtual MaterializedEnumerable<T> Materialize<T>(IEnumerable<T> items)
        {
            var bag = new ConcurrentBag<T>();
            foreach (var item in items)
            {
                bag.Add(item);
            }

            return new ConcurrentBagMaterializedEnumerable<T>(bag);
        }
    }
}
