namespace xofz.Framework.Theory
{
    using System;

    public class Functoid<T, K, V> where T: struct
    {
        public Functoid(Agent<T> agent, Func<K, V, T> functor)
        {
            this.agent = agent;
            this.functor = functor;
        }

        public virtual T Make(K source1, V source2)
        {
            var actee = this.functor(source1, source2);
            var tuple = this.agent.Act(actee);

            if (tuple.Item1 > 1000 * 1000 * 1000) // todo: fix these arbitrary values
            {
                return this.Make(source1, source2);
            }

            if (tuple.Item1 > 1000 * 1000)
            {
                return this.agent.Act(this.functor(source1, source2)).Item2;
            }

            if (tuple.Item1 > 1000)
            {
                return tuple.Item2;
            }

            return default(T);
        }

        private readonly Agent<T> agent;
        private readonly Func<K, V, T> functor;
    }
}
