namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using xofz.Framework.Lotters;

    public class LazyLot<T> : Lot<T>
    {
        public LazyLot(IEnumerable<T> source)
            : this(source, new LinkedListLotter())
        {
        }

        public LazyLot(
            IEnumerable<T> source, 
            Lotter lotter)
        {
            this.source = source;
            this.lotter = lotter;
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
                this.setItems(this.lotter.Materialize(this.source));
            }
        }

        private void setItems(Lot<T> items)
        {
            this.items = items;
        }

        private int materializedIf1;
        private Lot<T> items; 
        private readonly Lotter lotter;
        private readonly IEnumerable<T> source;
    }
}
