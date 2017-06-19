namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Materialization;

    public class CollectionHolder
    {
        public CollectionHolder()
            : this(new LinkedListMaterializer())
        {
        }

        public CollectionHolder(Materializer materializer)
        {
            this.materializer = materializer;
            this.collections = new List<Tuple<string, object>>(0x100);
        }

        public virtual void Register<T>(
            IEnumerable<T> source,
            string name = null)
        {
            object o = source;
            this.collections.Add(
                Tuple.Create(name, o));
        }

        public virtual MaterializedEnumerable<T> Get<T>(string name = null)
        {
            var collection = this.collections.FirstOrDefault(
                tuple => tuple.Item1 == name
                && tuple.Item2 is MaterializedEnumerable<T>);

            if (collection != null)
            {
                return collection.Item2 as MaterializedEnumerable<T>;
            }

            collection = this.collections.FirstOrDefault(
                tuple => tuple.Item1 == name
                         && tuple.Item2 is IEnumerable<T>);

            if (collection == null)
            {
                return default(MaterializedEnumerable<T>);
            }

            return this.materializer.Materialize(
                collection.Item2 as IEnumerable<T>);
        }

        private readonly Materializer materializer;
        private readonly List<Tuple<string, object>> collections;
    }
}
