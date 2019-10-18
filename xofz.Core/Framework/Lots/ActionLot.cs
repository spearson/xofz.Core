namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ActionLot<T>
        : Lot<T>
    {
        public ActionLot(
            IEnumerable<Action<T>> source, 
            Lot<T> items)
        {
            this.actionSource = source
                          ?? EnumerableHelpers.Empty<Action<T>>();
            this.items = items
                         ?? Lot.Empty<T>();
        }

        public virtual long Count => this.items.Count;

        public virtual IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.actionSource.GetEnumerator();
            foreach (var item in this.items)
            {
                enumerator.Current?.Invoke(item);
                yield return item;
                enumerator.MoveNext();
            }

            enumerator.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Contains(
            T item)
        {
            return this.items.Contains(item);
        }

        public virtual void CopyTo(
            T[] array)
        {
            var enumerator = this.actionSource.GetEnumerator();
            long indexer = 0;
            var l = array.Length;
            foreach (var item in this.items)
            {
                enumerator.MoveNext();
                enumerator.Current?.Invoke(item);
                array[indexer] = item;
                
                checked
                {
                    ++indexer;
                }

                if (indexer >= l)
                {
                    break;
                }
            }

            enumerator.Dispose();
        }

        protected readonly IEnumerable<Action<T>> actionSource;
        protected readonly Lot<T> items;
    }
}
