namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ValueKeyPairLot<K, V> : Lot<V>
    {
        public ValueKeyPairLot(Lot<KeyValuePair<K, V>> items)
        {
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
            return !this.items.FirstOrDefault(kvp => kvp.Value.Equals(value)).Equals(default(KeyValuePair<K, V>));
        }

        public void CopyTo(V[] valueArray)
        {
            var kvps = this.items;
            Array.Copy(kvps.Select(kvp => kvp.Value).ToArray(), valueArray, kvps.Count);
        }

        private readonly Lot<KeyValuePair<K, V>> items;
    }
}
