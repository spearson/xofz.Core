namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ActionMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public ActionMaterializedEnumerable(
            IEnumerable<Action<T>> source, MaterializedEnumerable<T> items)
        {
            this.source = source;
            this.items = items;
        }

        long MaterializedEnumerable<T>.Count => this.items.Count;

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.source.GetEnumerator();
            foreach (var item in this.items)
            {
                enumerator.Current(item);
                yield return item;
                enumerator.MoveNext();
            }

            enumerator.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            var enumerator = this.source.GetEnumerator();
            long counter = 0;
            foreach (var item in this.items)
            {
                enumerator.Current(item);
                array[counter] = item;
                enumerator.MoveNext();
                ++counter;
            }

            enumerator.Dispose();
        }

        private readonly IEnumerable<Action<T>> source;
        private readonly MaterializedEnumerable<T> items;
    }
}
