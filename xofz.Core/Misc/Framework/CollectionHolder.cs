namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lotters;

    public class CollectionHolder
    {
        public CollectionHolder()
            : this(new LinkedListLotter())
        {
        }

        public CollectionHolder(
            Lotter lotter)
        {
            this.lotter = lotter ?? new LinkedListLotter();
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

        public virtual Lot<T> Get<T>(
            string name = null)
        {
            var collection = this.collections.FirstOrDefault(
                tuple => tuple.Item1 == name
                && tuple.Item2 is Lot<T>);

            if (collection != null)
            {
                return collection.Item2 as Lot<T>;
            }

            collection = this.collections.FirstOrDefault(
                tuple => tuple.Item1 == name
                         && tuple.Item2 is IEnumerable<T>);

            if (collection == null)
            {
                return default;
            }

            return this.lotter.Materialize(
                collection.Item2 as IEnumerable<T>);
        }

        protected readonly Lotter lotter;
        protected readonly List<Tuple<string, object>> collections;
    }
}
