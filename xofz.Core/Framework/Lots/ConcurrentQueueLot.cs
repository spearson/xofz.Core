namespace xofz.Framework.Lots
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

        public virtual long Count => this.queue.Count;

        public virtual bool IsEmpty => this.queue.IsEmpty;

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.queue.CopyTo(array, 0);
        }

        public virtual void CopyTo(
            T[] array, 
            int index)
        {
            this.queue.CopyTo(array, index);
        }

        public virtual void Enqueue(
            T item)
        {
            this.queue.Enqueue(item);
        }

        public virtual T[] ToArray()
        {
            return this.queue.ToArray();
        }

        public virtual bool TryDequeue(
            out T result)
        {
            return this.queue.TryDequeue(out result);
        }

        public virtual bool TryPeek(
            out T result)
        {
            return this.queue.TryPeek(out result);
        }

        protected readonly ConcurrentQueue<T> queue;
    }
}
