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

        public virtual long Count => 1;

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

            if (array.Length < 1)
            {
                return;
            }

            array[0] = this.item;
        }

        public virtual bool Contains(
            T item)
        {
            return this.item.Equals(item);
        }

        protected readonly T item;
    }
}
