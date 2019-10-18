namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ConcurrentBagLot<T>
        : Lot<T>
    {
        public ConcurrentBagLot()
            : this(new ConcurrentBag<T>())
        {
        }

        public ConcurrentBagLot(
            IEnumerable<T> finiteSource)
            : this(new ConcurrentBag<T>(finiteSource))
        {
        }

        public ConcurrentBagLot(
            ConcurrentBag<T> bag)
        {
            if (bag == null)
            {
                bag = new ConcurrentBag<T>();
            }

            this.bag = bag;
        }

        public virtual long Count => this.bag.Count;

        public virtual bool IsEmpty => this.bag.IsEmpty;

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.bag.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual T[] ToArray()
        {
            return this.bag.ToArray();
        }

        public virtual bool TryPeek(
            out T item)
        {
            return this.bag.TryPeek(out item);
        }

        public virtual bool TryTake(
            out T item)
        {
            return this.bag.TryTake(out item);
        }

        public virtual void Add(T item)
        {
            this.bag.Add(item);
        }

        public virtual void CopyTo(
            T[] array,
            int index)
        {
            this.bag.CopyTo(array, index);
        }

        protected readonly ConcurrentBag<T> bag;
    }
}
