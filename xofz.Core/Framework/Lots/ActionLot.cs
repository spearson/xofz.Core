namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ActionLot<T> : Lot<T>
    {
        public ActionLot(
            IEnumerable<Action<T>> source, 
            Lot<T> items)
        {
            this.source = source;
            this.items = items;
        }

        public long Count => this.items.Count;

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.source.GetEnumerator();
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
            var enumerator = this.source.GetEnumerator();
            long counter = 0;
            foreach (var item in this.items)
            {
                enumerator.Current?.Invoke(item);
                array[counter] = item;
                enumerator.MoveNext();
                ++counter;
            }

            enumerator.Dispose();
        }

        protected readonly IEnumerable<Action<T>> source;
        protected readonly Lot<T> items;
    }
}
