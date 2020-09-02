namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using static EnumerableHelpers;
    
    public class KeyValuePairLot<K, V> 
        : Lot<K>
    {
        public KeyValuePairLot(
            Lot<KeyValuePair<K, V>> items)
        {
            this.items = items
                         ?? throw new ArgumentNullException(
                             nameof(items));
        }

        public virtual long Count => this.items.Count;

        public virtual IEnumerator<K> GetEnumerator()
        {
            return Select(
                    this.items,
                    i => i.Key)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Contains(
            K key)
        {
            return !FirstOrDefault(
                    this.items,
                    kvp => kvp.Key.Equals(key))
                .Equals(default(KeyValuePair<K, V>));
        }

        public virtual void CopyTo(
            K[] keyArray)
        {
            var kvps = this.items;
            Array.Copy(
                ToArray(
                    Select(
                        kvps,
                        kvp => kvp.Key)),
                keyArray,
                kvps.Count);
        }

        protected readonly Lot<KeyValuePair<K, V>> items;
    }
}
