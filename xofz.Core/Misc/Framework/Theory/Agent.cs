namespace xofz.Misc.Framework.Theory
{
    using System;
    using System.Numerics;

    public class Agent<T> where T: struct
    {
        public Agent(
            Action<BigInteger, T> operate,
            Random<BigInteger> rng)
        {
            this.operate = operate;
            this.rng = rng;
            this.randomness = int.MaxValue; // todo: expand!
        }

        public virtual Tuple<BigInteger, T> Act(T actee)
        {
            var randomNumber = this.rng.Next(this.randomness);
            this.operate(randomNumber, actee);

            return Tuple.Create(randomNumber, actee);
        }

        private readonly Action<BigInteger, T> operate;
        private readonly Random<BigInteger> rng;
        private readonly BigInteger randomness;
    }
}
