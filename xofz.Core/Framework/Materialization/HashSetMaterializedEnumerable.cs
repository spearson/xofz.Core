namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public sealed class HashSetMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public HashSetMaterializedEnumerable()
        {
            this.hashSet = new HashSet<T>();
        }

        public HashSetMaterializedEnumerable(HashSet<T> hashSet)
        {
            this.hashSet = hashSet;
        }

        long MaterializedEnumerable<T>.Count => this.hashSet.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            this.hashSet.Add(item);
        }

        public bool Remove(T item)
        {
            return this.hashSet.Remove(item);
        }

        public void Clear()
        {
            this.hashSet.Clear();
        }

        public bool Contains(T item)
        {
            return this.hashSet.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            this.hashSet.CopyTo(array);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.hashSet.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            this.hashSet.OnDeserialization(sender);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            this.hashSet.IntersectWith(other);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            this.hashSet.ExceptWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.hashSet.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.hashSet.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.hashSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.hashSet.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.hashSet.Overlaps(other);
        }

        public void RemoveWhere(Predicate<T> match)
        {
            this.hashSet.RemoveWhere(match);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.hashSet.SetEquals(other);
        }

        public void SymmetricExcepWith(IEnumerable<T> other)
        {
            this.hashSet.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            this.hashSet.UnionWith(other);
        }

        private readonly HashSet<T> hashSet;
    }
}
