namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public sealed class LazyMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public LazyMaterializedEnumerable(IEnumerable<T> source)
            : this(source, new LinkedListMaterializer())
        {
        }

        public LazyMaterializedEnumerable(
            IEnumerable<T> source, 
            Materializer materializer)
        {
            this.source = source;
            this.materializer = materializer;
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.checkItems();
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public long Count
        {
            get
            {
                this.checkItems();
                return this.items.Count;
            }
        }

        public void CopyTo(T[] array)
        {
            this.checkItems();
            new List<T>(this.items).CopyTo(array);
        }

        public bool Contains(T item)
        {
            this.checkItems();
            return this.items.Contains(item);
        }

        private void checkItems()
        {
            if (Interlocked.CompareExchange(ref this.materializedIf1, 1, 0) == 0)
            {
                this.setItems(this.materializer.Materialize(this.source));
            }
        }

        private void setItems(MaterializedEnumerable<T> items)
        {
            this.items = items;
        }

        private int materializedIf1;
        private MaterializedEnumerable<T> items; 
        private readonly Materializer materializer;
        private readonly IEnumerable<T> source;
    }
}
