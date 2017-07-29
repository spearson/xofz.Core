namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class EnumerableTrapper<T>
    {
        public virtual MaterializedEnumerable<T> TrappedCollection => 
            new LinkedListMaterializedEnumerable<T>(this.trapper);

        public virtual IEnumerable<T> Trap(IEnumerable<T> source)
        {
            var t = new LinkedList<T>();
            this.setTrapper(t);

            foreach (var item in source)
            {
                t.AddLast(item);
                yield return item;
            }
        }

        private void setTrapper(LinkedList<T> trapper)
        {
            this.trapper = trapper;
        }

        private LinkedList<T> trapper;
    }

    public class EnumerableTrapper
    {
        public EnumerableTrapper()
        {
            this.collections = new List<Tuple<
                string, object>>(0xFF);
        }

        public virtual MaterializedEnumerable<T> ReadTrappedCollection<T>(
            string collectionName = null)
        {
            foreach (var tuple in this.collections)
            {
                if (tuple.Item1 != collectionName)
                {
                    continue;
                }

                var t = tuple.Item2 as EnumerableTrapper<T>;
                if (t == default(EnumerableTrapper<T>))
                {
                    continue;
                }

                return t.TrappedCollection;
            }

            return default(MaterializedEnumerable<T>);
        }

        public virtual IEnumerable<T> Trap<T>(
            IEnumerable<T> source,
            string collectionName = null)
        {
            var t = new EnumerableTrapper<T>();
            this.collections.Add(
                Tuple.Create(
                    collectionName,
                    (object)t));

            return t.Trap(source);
        }

        private readonly IList<Tuple<string, object>> collections;
    }
}
