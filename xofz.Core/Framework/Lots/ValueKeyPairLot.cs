namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EH = xofz.EnumerableHelpers;

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
            return EH.Select(
                    this.items,
                    i => i.Value)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Contains(
            V value)
        {
            return EH.Contains(
                EH.Select(
                    this.items,
                    kvp => kvp.Value),
                value);
        }

        public virtual void CopyTo(
            V[] valueArray)
        {
            var lot = this.items;
            Array.Copy(
                EH.ToArray(
                    EH.Select(
                        lot,
                        kvp => kvp.Value)),
                valueArray,
                lot.Count);
        }

        protected readonly Lot<KeyValuePair<K, V>> items;
    }
}
