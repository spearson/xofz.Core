namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class HashSetLot<T>
        : Lot<T>
    {
        public HashSetLot()
            : this(new HashSet<T>())
        {
        }

        public HashSetLot(
            HashSet<T> hashSet)
        {
            this.hashSet = hashSet ??
                           throw new ArgumentNullException(
                               nameof(hashSet));
        }

        public virtual long Count => this.hashSet.Count;

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Add(
            T item)
        {
            return this.hashSet.Add(item);
        }

        public virtual bool Remove(
            T item)
        {
            return this.hashSet.Remove(item);
        }

        public virtual void Clear()
        {
            this.hashSet.Clear();
        }

        public virtual bool Contains(
            T item)
        {
            return this.hashSet.Contains(item);
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.hashSet.CopyTo(array);
        }

        public virtual void GetObjectData(
            SerializationInfo info, 
            StreamingContext context)
        {
            this.hashSet.GetObjectData(info, context);
        }

        public virtual void OnDeserialization(
            object sender)
        {
            this.hashSet.OnDeserialization(sender);
        }

        public virtual void IntersectWith(
            IEnumerable<T> other)
        {
            this.hashSet.IntersectWith(other);
        }

        public virtual void ExceptWith(
            IEnumerable<T> other)
        {
            this.hashSet.ExceptWith(other);
        }

        public virtual bool IsProperSubsetOf(
            IEnumerable<T> other)
        {
            return this.hashSet.IsProperSubsetOf(other);
        }

        public virtual bool IsProperSupersetOf(
            IEnumerable<T> other)
        {
            return this.hashSet.IsProperSupersetOf(other);
        }

        public virtual bool IsSubsetOf(
            IEnumerable<T> other)
        {
            return this.hashSet.IsSubsetOf(other);
        }

        public virtual bool IsSupersetOf(
            IEnumerable<T> other)
        {
            return this.hashSet.IsSupersetOf(other);
        }

        public virtual bool Overlaps(
            IEnumerable<T> other)
        {
            return this.hashSet.Overlaps(other);
        }

        public virtual void RemoveWhere(
            Predicate<T> match)
        {
            this.hashSet.RemoveWhere(match);
        }

        public virtual bool SetEquals(
            IEnumerable<T> other)
        {
            return this.hashSet.SetEquals(other);
        }

        public virtual void SymmetricExcepWith(
            IEnumerable<T> other)
        {
            this.hashSet.SymmetricExceptWith(other);
        }

        public virtual void UnionWith(
            IEnumerable<T> other)
        {
            this.hashSet.UnionWith(other);
        }

        protected readonly HashSet<T> hashSet;
    }
}
