namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using xofz.Framework.Lotters;

    public class ConcurrentDictionaryLot<TKey, TValue>
        : Lot<KeyValuePair<TKey, TValue>>
    {
        public ConcurrentDictionaryLot()
            : this(
                new ConcurrentDictionary<TKey, TValue>(),
                new LinkedListLotter())
        {
        }

        public ConcurrentDictionaryLot(
            IEnumerable<KeyValuePair<TKey, TValue>> source)
            : this(
                source,
                new LinkedListLotter())
        {
        }

        public ConcurrentDictionaryLot(
            Lotter lotter)
            : this(
                new ConcurrentDictionary<TKey, TValue>(),
                lotter)
        {
        }

        public ConcurrentDictionaryLot(
            ConcurrentDictionary<TKey, TValue> dictionary)
            : this(
                dictionary,
                new LinkedListLotter())
        {
        }

        public ConcurrentDictionaryLot(
            IEnumerable<KeyValuePair<TKey, TValue>> source,
            Lotter lotter)
            : this(
                new ConcurrentDictionary<TKey, TValue>(source),
                lotter)
        {
        }

        public ConcurrentDictionaryLot(
            ConcurrentDictionary<TKey, TValue> dictionary,
            Lotter lotter)
        {
            this.dictionary = dictionary;
            this.lotter = lotter;
        }

        public long Count
            => this.dictionary.Count;

        public Lot<TKey> Keys =>
            this.lotter.Materialize(this.dictionary.Keys);

        public Lot<TValue> Values =>
            this.lotter.Materialize(this.dictionary.Values);

        public bool IsEmpty => this.dictionary.IsEmpty;

        public TValue this[TKey key] => this.dictionary[key];

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public TValue AddOrUpdate(
            TKey key,
            TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            return this.dictionary.AddOrUpdate(
                key,
                addValue,
                updateValueFactory);
        }

        public TValue AddOrUpdate(
            TKey key,
            Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            return this.dictionary.AddOrUpdate(
                key,
                addValueFactory,
                updateValueFactory);
        }

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return this.dictionary.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return this.dictionary.GetOrAdd(key, valueFactory);
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            return this.dictionary.ToArray();
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return this.dictionary.TryAdd(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            return this.dictionary.TryRemove(key, out value);
        }

        public bool TryUpdate(TKey key, TValue value, TValue comparisonValue)
        {
            return this.dictionary.TryUpdate(key, value, comparisonValue);
        }

        private readonly ConcurrentDictionary<TKey, TValue> dictionary;
        private readonly Lotter lotter;
    }
}
