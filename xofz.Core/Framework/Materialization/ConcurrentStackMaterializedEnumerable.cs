namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ConcurrentStackMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public ConcurrentStackMaterializedEnumerable()
        {
            this.stack = new ConcurrentStack<T>();
        }

        public ConcurrentStackMaterializedEnumerable(IEnumerable<T> source)
        {
            this.stack = new ConcurrentStack<T>(source);
        }

        public ConcurrentStackMaterializedEnumerable(ConcurrentStack<T> stack)
        {
            this.stack = stack;
        }

        public long Count => this.stack.Count;

        public bool IsEmpty => this.stack.IsEmpty;

        public IEnumerator<T> GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            this.stack.Clear();
        }

        public void CopyTo(T[] array)
        {
            this.stack.CopyTo(array, 0);
        }

        public void CopyTo(T[] array, int index)
        {
            this.stack.CopyTo(array, index);
        }

        public void Push(T item)
        {
            this.stack.Push(item);
        }

        public void PushRange(IEnumerable<T> source)
        {
            MaterializedEnumerable<T> ll =
                new LinkedListMaterializedEnumerable<T>(source);
            var array = new T[ll.Count];
            long counter = 0;
            foreach (var item in ll)
            {
                array[counter] = item;
                ++counter;
            }

            this.stack.PushRange(array);
        }

        public void PushRange(IEnumerable<T> source, long index, long length)
        {
            MaterializedEnumerable<T> ll =
                new LinkedListMaterializedEnumerable<T>(source);
            var array = new T[length - index];
            long counter = 0;
            foreach (var item in ll)
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

        public T[] ToArray()
        {
            return this.stack.ToArray();
        }

        public bool TryPeek(out T result)
        {
            return this.stack.TryPeek(out result);
        }

        public bool TryPop(out T result)
        {
            return this.stack.TryPop(out result);
        }

        public int TryPopAll(out T[] poppedItems)
        {
            var s = this.stack;
            poppedItems = new T[s.Count];
            return s.TryPopRange(poppedItems);
        }

        public int TryPopRange(out T[] poppedItems, int index, int count)
        {
            var s1 = this.stack;
            var s2 = new ConcurrentStack<T>(
                s1.Skip(index).Take(count));
            poppedItems = new T[count];
            return s2.TryPopRange(poppedItems);
        }

        private readonly ConcurrentStack<T> stack;
    }
}
