namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class FuncLot<T> 
        : Lot<T>
    {
        public FuncLot(
            ICollection<Func<T>> collection)
        {
            this.collection = collection;
        }

        public virtual long Count => this.collection.Count;

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this
                .collection
                .Select(
                    func => func())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Contains(
            T item)
        {
            var array = new T[this.Count];
            this.CopyTo(array);

            return new Collection<T>(array).Contains(item);
        }

        public virtual void CopyTo(
            T[] array)
        {
            long counter = 0;
            foreach (var func in this.collection)
            {
                array[counter] = func();
                ++counter;
            }
        }

        protected readonly ICollection<Func<T>> collection;
    }
}
