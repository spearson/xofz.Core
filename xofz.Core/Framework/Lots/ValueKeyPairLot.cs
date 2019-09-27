namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueKeyPairLot<K, V> 
        : Lot<V>
    {
        public ValueKeyPairLot(
            Lot<KeyValuePair<K, V>> items)
        {
            this.items = items
                         ?? throw new ArgumentNullException(
                             nameof(items));
        }

        public virtual long Count => this.items.Count;

        public virtual IEnumerator<V> GetEnumerator()
        {
            return this
                .items
                .Select(i => i.Value)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Contains(
            V value)
        {
            return EnumerableHelpers.Contains(
                this.items.Select(kvp => kvp.Value),
                value);
        }

        public virtual void CopyTo(
            V[] valueArray)
        {
            var lot = this.items;
            Array.Copy(
                lot.Select(kvp => kvp.Value).ToArray(), 
                valueArray, 
                lot.Count);
        }

        protected readonly Lot<KeyValuePair<K, V>> items;
    }
}
