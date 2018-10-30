﻿namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ConcurrentQueueLot<T> 
        : Lot<T>
    {
        public ConcurrentQueueLot()
            : this(new ConcurrentQueue<T>())
        {
        }

        public ConcurrentQueueLot(IEnumerable<T> source)
            : this(new ConcurrentQueue<T>(source))
        {
        }

        public ConcurrentQueueLot(
            ConcurrentQueue<T> queue)
        {
            this.queue = queue;
        }

        public long Count => this.queue.Count;

        public bool IsEmpty => this.queue.IsEmpty;

        public IEnumerator<T> GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CopyTo(T[] array)
        {
            this.queue.CopyTo(array, 0);
        }

        public void CopyTo(T[] array, int index)
        {
            this.queue.CopyTo(array, index);
        }

        public void Enqueue(T item)
        {
            this.queue.Enqueue(item);
        }

        public T[] ToArray()
        {
            return this.queue.ToArray();
        }

        public bool TryDequeue(out T result)
        {
            return this.queue.TryDequeue(out result);
        }

        public bool TryPeek(out T result)
        {
            return this.queue.TryPeek(out result);
        }

        private readonly ConcurrentQueue<T> queue;
    }
}
