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
            this.onBitCount = initialPool.Count(b => b);
        }

        public UnifiedBitPool(int onBitCount)
            : this(onBitCount, new LinkedListLotter())
        {
        }

        public UnifiedBitPool(
            int onBitCount, 
            Lotter lotter)
        {
            this.onBitCount = onBitCount;
            this.lotter = lotter;
            var max = onBitCount * 2;
            var array = new bool[max];
            for (long i = 0; i < max - 1; i += 2)
            {
                array[i] = true;
                array[i + 1] = false;
            }

            this.currentPool = lotter.Materialize(array);
        }

        public virtual int OnBitCount => this.onBitCount;

        public virtual int PoolSize => (int)this.currentPool.Count;

        public virtual void Shift(
            Func<Lot<bool>, Lot<bool>> shifter)
        {
            this.setPool(shifter(this.currentPool));
        }

        public virtual void Relocate(
            Func<long, bool, long> relocator)
        {
            var cp = this.currentPool;
            var array = new bool[cp.Count];
            var e = cp.GetEnumerator();
            for (long i = 0; i < cp.Count; ++i)
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

        private void setPool(Lot<bool> newPool)
        {
            if (newPool.Count(b => b) != this.onBitCount)
            {
                throw new InvalidOperationException(
                    "delegate must retain current on bit count.");
            }

            this.currentPool = newPool;
        }

        private Lot<bool> currentPool;
        private readonly int onBitCount;
        private readonly Lotter lotter;
    }

    public class Tester
    {
        public void Go()
        {
            var ubp = new UnifiedBitPool(10);
            Console.WriteLine(ubp.OnBitCount);
            Console.WriteLine();

            foreach (var index in ubp.ReadOnBitIndexes())
            {
                Console.WriteLine(index);
            }
            Console.WriteLine();

            ubp.Relocate((i, on) =>
            {
                if (i % 2 == 1 && i < ubp.OnBitCount - 1)
                {
                    return i + 1;
                }

                if (i % 2 == 1)
                {
                    return 0;
                }

                if (i % 2 == 0)
                {
                    return i + 1;
                }

                throw new InvalidOperationException(
                    "Total bit count must be even for this!");
            });
            foreach (var index in ubp.ReadOnBitIndexes())
            {
                Console.WriteLine(index);
            }
            Console.WriteLine();

            // now try to move back
            ubp.Relocate((i, on) =>
            {
                if (on)
                {
                    return i - 1;
                }

                return i + 1;
            });
            foreach (var index in ubp.ReadOnBitIndexes())
            {
                Console.WriteLine(index);
            }
            Console.WriteLine();

            var r = new Random();
            var availableIndexes
                = new List<int>(Enumerable.Range(0, ubp.PoolSize));
            ubp.Relocate((i, on) =>
            {
                var newIndex = r.Next(0, ubp.PoolSize);
                while (!availableIndexes.Contains(newIndex))
                {
                    newIndex = r.Next(0, ubp.PoolSize);
                }

                availableIndexes.Remove(newIndex);
                return newIndex;
            });
            foreach (var index in ubp.ReadOnBitIndexes())
            {
                Console.WriteLine(index);
            }
            Console.WriteLine();
        }
    }
}
