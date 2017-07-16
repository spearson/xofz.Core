namespace xofz.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Reflection<T>
    {
        public Reflection(MaterializedEnumerable<T> collection, Absolution<T> absolution)
        {
            this.collection = collection;
            this.absolution = absolution;
        }

        public virtual IEnumerable<T> Reflect(Action<T> learn)
        {
            var enumerator = this.collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var unknown1 = enumerator.Current;
                enumerator.MoveNext();
                var unknown2 = enumerator.Current;
                var tuple = this.absolution.Absolve(this.createFactory(unknown1, unknown2), learn);
                yield return tuple.Item1;
                yield return tuple.Item2;
            }

            enumerator.Dispose();
        }

        private Func<T> createFactory(T item1, T item2)
        {
            this.currentItem = item1;
            return () =>
            {
                var item = this.currentItem;
                this.currentItem = item2;
                return item;
            };
        }

        private T currentItem;
        private readonly MaterializedEnumerable<T> collection;
        private readonly Absolution<T> absolution;
    }
}
