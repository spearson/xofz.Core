﻿namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;

    public class StackMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public StackMaterializedEnumerable()
        {
            this.stack = new Stack<T>();
        }

        public StackMaterializedEnumerable(Stack<T> stack)
        {
            this.stack = stack;
        }

        long MaterializedEnumerable<T>.Count => this.stack.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            return this.stack.ToArray();
        }

        public T Peek()
        {
            return this.stack.Peek();
        }

        public T Pop()
        {
            return this.stack.Pop();
        }

        public bool Contains(T item)
        {
            return this.stack.Contains(item);
        }

        public void Clear()
        {
            this.stack.Clear();
        }

        public void CopyTo(T[] array)
        {
            this.stack.CopyTo(array, 0);
        }

        public void Push(T item)
        {
            this.stack.Push(item);
        }

        public void TrimExcess()
        {
            this.stack.TrimExcess();
        }

        private readonly Stack<T> stack;
    }
}
