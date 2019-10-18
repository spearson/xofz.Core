namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using xofz.Framework.Lotters;

    public class UnifiedBitPool
    {
        public UnifiedBitPool(
            Lot<bool> initialPool)
            : this(initialPool, new LinkedListLotter())
        {
        }

        public UnifiedBitPool(
            Lot<bool> initialPool,
            Lotter lotter)
        {
            Debug.Assert(
                initialPool != default(Lot<bool>));
            this.currentPool = initialPool;
            this.lotter = lotter;
            this.noOfOnBits = initialPool.Count(b => b);
        }

        public UnifiedBitPool(
            int noOfOnBits)
            : this(noOfOnBits, new LinkedListLotter())
        {
        }

        public UnifiedBitPool(
            int noOfOnBits, 
            Lotter lotter)
        {
            this.noOfOnBits = noOfOnBits;
            this.lotter = lotter;
            var max = noOfOnBits * 2;
            var array = new bool[max];
            for (long i = 0; i < max - 1; i += 2)
            {
                array[i] = true;
                array[i + 1] = false;
            }

            this.currentPool = lotter.Materialize(array);
        }

        public virtual int OnBitCount => this.noOfOnBits;

        public virtual int PoolSize => (int)this.currentPool.Count;

        public virtual void Shift(
            Func<Lot<bool>, Lot<bool>> shifter)
        {
            if (shifter == null)
            {
                return;
            }

            this.setPool(shifter(this.currentPool));
        }

        public virtual void Relocate(
            Func<long, bool, long> relocator)
        {
            if (relocator == null)
            {
                return;
            }

            var cp = this.currentPool;
            var cpc = cp.Count;
            var array = new bool[cpc];
            var e = cp.GetEnumerator();
            for (long i = 0; i < cpc; ++i)
            {
                e.MoveNext();
                var newIndex = relocator(i, e.Current);
                array[newIndex] = e.Current;
            }

            e.Dispose();

            this.setPool(
                this.lotter.Materialize(array));
        }

        public virtual IEnumerable<long> ReadOnBitIndexes()
        {
            var cp = this.currentPool;
            long currentIndex = 0;
            foreach (var bit in cp)
            {
                if (bit)
                {
                    yield return currentIndex;
                }

                ++currentIndex;
            }
        }

        protected virtual void setPool(
            Lot<bool> newPool)
        {
            if (newPool.Count(b => b) != this.noOfOnBits)
            {
                throw new InvalidOperationException(
                    @"delegate must retain current on bit count.");
            }

            this.currentPool = newPool;
        }

        protected Lot<bool> currentPool;
        protected  readonly int noOfOnBits;
        protected readonly Lotter lotter;
    }
}
