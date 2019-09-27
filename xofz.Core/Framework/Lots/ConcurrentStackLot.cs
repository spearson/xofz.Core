namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class ConcurrentStackLot<T> 
        : Lot<T>
    {
        public ConcurrentStackLot()
            : this(
                new ConcurrentStack<T>())
        {
        }

        public ConcurrentStackLot(
            IEnumerable<T> finiteSource)
            : this(
                new ConcurrentStack<T>(
                    finiteSource))
        {
        }

        public ConcurrentStackLot(
            ConcurrentStack<T> stack)
        {
            this.stack = stack;
        }

        public virtual long Count => this.stack.Count;

        public virtual bool IsEmpty => this.stack.IsEmpty;

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Clear()
        {
            this.stack.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.stack.CopyTo(array, 0);
        }

        public virtual void CopyTo(
            T[] array, 
            int index)
        {
            this.stack.CopyTo(array, index);
        }

        public virtual void Push(
            T item)
        {
            this.stack.Push(item);
        }

        public virtual void PushRange(T[] items)
        {
            this.stack.PushRange(items);
        }

        public void PushRange(
            IEnumerable<T> source)
        {
            Lot<T> lot = new LinkedListLot<T>(source);
            var array = new T[lot.Count];
            long counter = 0;
            foreach (var item in lot)
            {
                array[counter] = item;
                ++counter;
            }

            this.stack.PushRange(array);
        }

        public virtual void PushRange(
            T[] items, 
            int startIndex, 
            int count)
        {
            this.stack.PushRange(
                items, 
                startIndex, 
                count);
        }

        public virtual void PushRange(
            IEnumerable<T> source, 
            long index, 
            long length)
        {
            Lot<T> lot =
                new LinkedListLot<T>(source);
            var array = new T[length - index];
            long counter = 0;
            foreach (var item in lot)
            {
                if (counter < index)
                {
                    continue;
                }

                if (counter >= length)
                {
                    break;
                }

                array[counter] = item;
                ++counter;
            }

            this.stack.PushRange(array);
        }

        public virtual T[] ToArray()
        {
            return this.stack.ToArray();
        }

        public virtual bool TryPeek(
            out T result)
        {
            return this.stack.TryPeek(
                out result);
        }

        public virtual bool TryPop(
            out T result)
        {
            return this.stack.TryPop(
                out result);
        }

        public virtual int TryPopAll(
            out T[] poppedItems)
        {
            var s = this.stack;
            poppedItems = new T[s.Count];
            return s.TryPopRange(poppedItems);
        }

        public virtual int TryPopRange(
            out T[] poppedItems, 
            int index, 
            int count)
        {
            var s1 = this.stack;
            var s2 = new ConcurrentStack<T>(
                s1.Skip(index).Take(count));
            poppedItems = new T[count];
            return s2.TryPopRange(poppedItems);
        }

        protected readonly ConcurrentStack<T> stack;
    }
}
