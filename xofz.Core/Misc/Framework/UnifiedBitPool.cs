namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using xofz.Framework.Materialization;

    public class UnifiedBitPool
    {
        public UnifiedBitPool(
            MaterializedEnumerable<bool> initialPool)
            : this(initialPool, new LinkedListMaterializer())
        {
        }

        public UnifiedBitPool(
            MaterializedEnumerable<bool> initialPool,
            Materializer materializer)
        {
            Debug.Assert(
                initialPool != default(MaterializedEnumerable<bool>));
            this.currentPool = initialPool;
            this.materializer = materializer;
            this.onBitCount = initialPool.Count(b => b);
        }

        public UnifiedBitPool(int onBitCount)
            : this(onBitCount, new LinkedListMaterializer())
        {
        }

        public UnifiedBitPool(int onBitCount, Materializer materializer)
        {
            this.onBitCount = onBitCount;
            this.materializer = materializer;
            var max = onBitCount * 2;
            var array = new bool[max];
            for (long i = 0; i < max - 1; i += 2)
            {
                array[i] = true;
                array[i + 1] = false;
            }

            this.currentPool = materializer.Materialize(array);
        }

        public virtual int OnBitCount => this.onBitCount;

        public virtual int PoolSize => (int)this.currentPool.Count;

        public virtual void Shift(Func<MaterializedEnumerable<bool>,
            MaterializedEnumerable<bool>> shifter)
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
                this.materializer.Materialize(array));
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

        private void setPool(MaterializedEnumerable<bool> newPool)
        {
            if (newPool.Count(b => b) != this.onBitCount)
            {
                throw new InvalidOperationException(
                    "delegate must retain current on bit count.");
            }

            this.currentPool = newPool;
        }

        private MaterializedEnumerable<bool> currentPool;
        private readonly int onBitCount;
        private readonly Materializer materializer;
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
