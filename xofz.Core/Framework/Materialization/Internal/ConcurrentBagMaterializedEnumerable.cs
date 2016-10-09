namespace xofz.Framework.Materialization.Internal
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal sealed class ConcurrentBagMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public ConcurrentBagMaterializedEnumerable()
        {
            this.bag = new ConcurrentBag<T>();
        }

        public ConcurrentBagMaterializedEnumerable(ConcurrentBag<T> bag)
        {
            this.bag = bag;
        }

        long MaterializedEnumerable<T>.Count => this.bag.Count;

        public bool IsEmpty => this.bag.IsEmpty;

        public IEnumerator<T> GetEnumerator()
        {
            return this.bag.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            return this.bag.ToArray();
        }

        public bool TryPeek(out T result)
        {
            return this.bag.TryPeek(out result);
        }

        public bool TryTake(out T result)
        {
            return this.bag.TryTake(out result);
        }

        public bool Contains(T item)
        {
            return new List<T>(this.bag).Contains(item);
        }

        public void Add(T item)
        {
            this.bag.Add(item);
        }

        public void CopyTo(T[] array)
        {
            this.bag.CopyTo(array, 0);
        }

        private readonly ConcurrentBag<T> bag;
    }
}
