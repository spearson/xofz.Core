namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class Accumulator<TIn, TOut>
    {
        public Accumulator(
            Func<TIn, TIn> inFactory,
            Func<TIn, TOut> outFactory)
        {
            this.inFactory = inFactory;
            this.outFactory = outFactory;
        }

        public virtual IEnumerable<TOut> Accumulate(
            Action<TIn, TOut> striker, 
            TIn seed,
            BigInteger length)
        {
            BigInteger counter = 0;
            while (counter < length)
            {
                yield return this.accumulateNext(striker, seed);

                seed = this.inFactory(seed);
                ++counter;
            }
        }

        private TOut accumulateNext(
            Action<TIn, TOut> striker, 
            TIn currentSeed)
        {
            var element = this.outFactory(currentSeed);
            striker(currentSeed, element);

            return element;
        }

        private readonly Func<TIn, TIn> inFactory;
        private readonly Func<TIn, TOut> outFactory;
    }
}
