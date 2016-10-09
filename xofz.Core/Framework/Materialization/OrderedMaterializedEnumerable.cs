namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class OrderedMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public OrderedMaterializedEnumerable(IReadOnlyList<T> list)
        {
            this.list = list;
        }

        public T this[int index] => this.list[index];

        public long Count => this.list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            var count = this.Count;
            var items = this.list;
            for (var i = 0; i < count; ++i)
            {
                array[i] = items[i];
            }
        }

        private readonly IReadOnlyList<T> list;
    }
}
