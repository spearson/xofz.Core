namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using xofz.Framework.Lotters;

    public class LazyLot<T> : Lot<T>
    {
        public LazyLot(
            IEnumerable<T> finiteSource)
            : this(finiteSource, 
                new LinkedListLotter())
        {
        }

        public LazyLot(
            IEnumerable<T> finiteSource, 
            Lotter lotter)
        {
            this.finiteSource = finiteSource;
            this.lotter = lotter;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            this.checkItems();

            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual long Count
        {
            get
            {
                this.checkItems();

                return this.items.Count;
            }
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.checkItems();
            new List<T>(this.items)
                .CopyTo(array);
        }

        public virtual bool Contains(
            T item)
        {
            this.checkItems();
            return this.items.Contains(item);
        }

        protected virtual void checkItems()
        {
            if (Interlocked.CompareExchange(
                    ref this.materializedIf1,
                    1,
                    0) != 1)
            {
                this.setItems(
                    this.lotter.Materialize(
                        this.finiteSource));
            }
        }

        protected virtual void setItems(
            Lot<T> items)
        {
            this.items = items;
        }

        protected long materializedIf1;
        protected Lot<T> items; 
        protected readonly Lotter lotter;
        protected readonly IEnumerable<T> finiteSource;
    }
}
