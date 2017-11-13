namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public sealed class ConcurrentDictionaryMaterializedEnumerable<TKey, TValue>
        : MaterializedEnumerable<KeyValuePair<TKey, TValue>>
    {
        public ConcurrentDictionaryMaterializedEnumerable()
            : this(
                new ConcurrentDictionary<TKey, TValue>(),
                new LinkedListMaterializer())
        {
        }

        public ConcurrentDictionaryMaterializedEnumerable(
            IEnumerable<KeyValuePair<TKey, TValue>> source)
            : this(
                  source,
                  new LinkedListMaterializer())
        {
        }

        public ConcurrentDictionaryMaterializedEnumerable(
            Materializer materializer)
            : this(
                new ConcurrentDictionary<TKey, TValue>(),
                materializer)
        {
        }

        public ConcurrentDictionaryMaterializedEnumerable(
            ConcurrentDictionary<TKey, TValue> dictionary)
            : this(
                dictionary,
                new LinkedListMaterializer())
        {
        }

        public ConcurrentDictionaryMaterializedEnumerable(
            IEnumerable<KeyValuePair<TKey, TValue>> source,
            Materializer materializer)
            : this(
                new ConcurrentDictionary<TKey, TValue>(source),
                materializer)
        {
        }

        public ConcurrentDictionaryMaterializedEnumerable(
            ConcurrentDictionary<TKey, TValue> dictionary,
            Materializer materializer)
        {
            this.dictionary = dictionary;
            this.materializer = materializer;
        }

        long MaterializedEnumerable<KeyValuePair<TKey, TValue>>.Count
            => this.dictionary?.Count ?? 0;

        public MaterializedEnumerable<TKey> Keys =>
            this.materializer.Materialize(this.dictionary?.Keys);

        public MaterializedEnumerable<TValue> Values =>
            this.materializer.Materialize(this.dictionary?.Values);

        public bool IsEmpty => this.dictionary?.IsEmpty ?? true;

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
        private readonly Materializer materializer;
    }
}
