﻿namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class KeyValuePairMaterializedEnumerable<K, V> : MaterializedEnumerable<K>
    {
        public KeyValuePairMaterializedEnumerable(MaterializedEnumerable<KeyValuePair<K, V>> items)
        {
            this.items = items;
        }

        public long Count => this.items.Count;

        public IEnumerator<K> GetEnumerator()
        {
            return this.items.Select(i => i.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(K key)
        {
            return !this.items.FirstOrDefault(kvp => kvp.Key.Equals(key)).Equals(default(KeyValuePair<K, V>));
        }

        public void CopyTo(K[] keyArray)
        {
            var kvps = this.items;
            Array.Copy(kvps.Select(kvp => kvp.Key).ToArray(), keyArray, kvps.Count);
        }

        private readonly MaterializedEnumerable<KeyValuePair<K, V>> items;
    }
}
