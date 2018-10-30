namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueKeyPairLot<K, V> : Lot<V>
    {
        public ValueKeyPairLot(Lot<KeyValuePair<K, V>> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(
                    nameof(items));
            }

            this.items = items;
        }

        public long Count => this.items.Count;

        public IEnumerator<V> GetEnumerator()
        {
            return this.items.Select(i => i.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(V value)
        {
            return EnumerableHelpers.Contains(
                this.items.Select(kvp => kvp.Value),
                value);
        }

        public void CopyTo(V[] valueArray)
        {
            var lot = this.items;
            Array.Copy(
                lot.Select(kvp => kvp.Value).ToArray(), 
                valueArray, 
                lot.Count);
        }

        private readonly Lot<KeyValuePair<K, V>> items;
    }
}
