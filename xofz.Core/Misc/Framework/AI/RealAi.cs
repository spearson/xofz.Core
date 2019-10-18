namespace xofz.Misc.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Threading;
    using xofz.Framework.Lots;
    using xofz.Framework.Transformation;

    public class RealAi<T>
    {
        public RealAi(
            Tree<Action<T>> computations, 
            EnumerableRotator rotator)
        {
            this.computations = computations;
            this.rotator = rotator;
        }

        public virtual T Spin(
            Random<T> selector, 
            BigInteger release)
        {
            var item = selector.Next(release);
            foreach (var computation in this.computations)
            {
                computation(item);
            }

            return item;
        }

        public Lot<Action<T>> PeekComputations(
            BigInteger numberToPeek)
        {
            var ll = new LinkedList<Action<T>>();
            var enumerator = this.computations.GetEnumerator();
            enumerator.MoveNext();
            for (var i = 0; i < numberToPeek; ++i)
            {
                ll.AddLast(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    break;
                }
            }

            enumerator.Dispose();
            return new LinkedListLot<Action<T>>(ll);
        }

        public virtual void DoWork(
            IEnumerable<T> source, 
            Action<T> longFunction)
        {
            var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            Lot<Thread> threads = new LinkedListLot<Thread>();
            foreach (var computation in this.computations)
            {
                var current = enumerator.Current;
                computation(current);
                var ts = new LinkedList<Thread>();
                ts.AddLast(new Thread(() => longFunction(current)));
                ts.First.Value.Start();
                threads = this.rotator.Rotate(ts, 1);
                if (!enumerator.MoveNext())
                {
                    break;
                }
            }

            foreach (var t in threads)
            {
                t.Join();
            }

            enumerator.Dispose();
        }

        protected readonly Tree<Action<T>> computations;
        protected readonly EnumerableRotator rotator;
    }
}
