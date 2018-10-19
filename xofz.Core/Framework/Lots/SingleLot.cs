namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class SingleLot<T> : Lot<T>
    {
        public SingleLot(T item)
        {
            this.item = item;
        }

        long Lot<T>.Count => 1;

        public IEnumerator<T> GetEnumerator()
        {
            yield return this.item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CopyTo(T[] array)
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

        public bool Contains(T item)
        {
            return this.item.Equals(item);
        }

        private readonly T item;
    }
}
