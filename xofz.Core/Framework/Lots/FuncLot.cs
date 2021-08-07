namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using static EnumerableHelpers;

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
            return Select(this.collection,
                    func =>
                    {
                        if (func == null)
                        {
                            return default;
                        }

                        return func();
                    })
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
            const byte zero = 0;
            long counter = zero;
            foreach (var func in this.collection)
            {
                T item;
                if (func == null)
                {
                    item = default;
                    goto setArray;
                }

                item = func();

                setArray:
                array[counter] = item;
                ++counter;
            }
        }

        protected readonly ICollection<Func<T>> collection;
    }
}
