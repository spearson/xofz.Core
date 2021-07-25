namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class SingleLot<T>
        : Lot<T>
    {
        public SingleLot(
            T item)
        {
            this.item = item;
        }

        public virtual long Count => one;

        public virtual IEnumerator<T> GetEnumerator()
        {
            yield return this.item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void CopyTo(
            T[] array)
        {
            if (array == null)
            {
                return;
            }

            if (array.Length < one)
            {
                return;
            }

            array[zero] = this.item;
        }

        public virtual bool Contains(
            T item)
        {
            return this.item?.Equals(item)
                ?? item == null;
        }

        public override int GetHashCode()
        {
            return this.item?.GetHashCode()
                   ?? zero;
        }

        public override bool Equals(
            object obj)
        {
            return this.item?.Equals(obj)
                   ?? obj == null;
        }

        protected readonly T item;
        protected const byte 
            zero = 0,
            one = 1;
    }
}
