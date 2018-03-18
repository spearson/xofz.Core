namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class PrimeGenerator
    {
        public PrimeGenerator()
        {
            this.tester = new PrimeTester();
            this.currentSet = new LinkedList<long>(new long[] { 2, 3 });
        }

        public PrimeGenerator(LinkedList<long> currentSet)
        {
            this.tester = new PrimeTester();
            this.currentSet = currentSet;
        }

        public LinkedList<long> CurrentSet => this.currentSet;

        public virtual long NextPrime()
        {
            return this.collectPrime();
        }

        public virtual IEnumerable<long> Generate()
        {
            foreach (var prime in this.currentSet)
            {
                yield return prime;
            }

            while (true)
            {
                yield return this.collectPrime();
            }
        }

        private long collectPrime()
        {
            var cs = this.currentSet;
            var t = this.tester;
            cs.AddLast(cs.Last.Value + 2);
            while (!t.RelativelyPrime(cs, true))
            {
                var node = cs.Last;
                cs.RemoveLast();
                cs.AddLast(node.Value + 2);
            }

            return cs.Last.Value;
        }

        private readonly LinkedList<long> currentSet;
        private readonly PrimeTester tester;
    }
}
