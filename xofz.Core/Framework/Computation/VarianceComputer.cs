namespace xofz.Framework.Computation
{
    using System;
    using System.Numerics;

    public class VarianceComputer<T>
    {
        public VarianceComputer(Func<T, BigInteger> magnitudeComputer)
        {
            this.magnitudeComputer = magnitudeComputer;
        }

        public virtual BigInteger Compute(Action<T> act, T actor)
        {
            var mc = this.magnitudeComputer;
            var original = mc(actor);
            act(actor);
            var altered = mc(actor);

            return BigInteger.Abs(original - altered);
        }

        private readonly Func<T, BigInteger> magnitudeComputer;
    }
}
