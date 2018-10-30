namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class FuncLot<T> : Lot<T>
    {
        public FuncLot(IEnumerable<Func<T>> source)
        {
            this.source = source;
        }

        public long Count => this.source.Count();

        public IEnumerator<T> GetEnumerator()
        {
            return this.source.Select(func => func()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(T item)
        {
            var array = new T[this.Count];
            this.CopyTo(array);

            return new Collection<T>(array).Contains(item);
        }

        public void CopyTo(T[] array)
        {
            long counter = 0;
            foreach (var func in this.source)
            {
                array[counter] = func();
                ++counter;
            }
        }

        private readonly IEnumerable<Func<T>> source;
    }
}
