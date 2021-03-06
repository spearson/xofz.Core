namespace xofz.Misc.Framework.Computation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Imploder<T>
        : GetArray<T>
    {
        public Imploder(
            int capacity)
            : this(new List<T>(capacity), capacity)
        {
        }

        public Imploder(
            IList<T> list,
            int capacity)
        {
            this.list = list;
            this.capacity = capacity;
        }

        public virtual T this[long index] => this.list[(int)index];

        long Lot<T>.Count => this.list.Count;

        public virtual int CurrentCount => this.list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void AddItem(
            T item, 
            bool rightOverLeft)
        {
            var l = this.list;
            l.Insert(rightOverLeft ? l.Count - 1 : 0, item);
            if (l.Count > this.capacity)
            {
                bool even = l.Count % 2 == 0;
                var middle = l.Count / 2;
                var index = rightOverLeft
                    ? middle - (even ? 1 : 0)
                    : middle + (even ? 0 : 1);
                var target = l[index];
                l.RemoveAt(index);
                (target as IDisposable)?.Dispose();
            }
        }

        protected readonly IList<T> list;
        protected readonly int capacity;
    }
}
