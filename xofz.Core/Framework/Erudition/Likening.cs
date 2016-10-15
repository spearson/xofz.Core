namespace xofz.Framework.Erudition
{
    using System;
    using System.Linq;
    using Materialization;

    public class Likening
    {
        public virtual T Liken<T>(
            Func<T> factory, 
            MaterializedEnumerable<Action<T>> acts,
            long limiter)
        {
            var actee = factory();
            acts.First()(actee);
            var firstHashCode = actee.GetHashCode();
            foreach (var act in acts.Skip(1))
            {
                act(actee);
                if (Math.Abs(actee.GetHashCode() - firstHashCode) <= limiter)
                {
                    continue;
                }

                return this.Liken(
                    factory,
                    new LinkedListMaterializedEnumerable<Action<T>>(
                        acts.Except(new[] { act })),
                    limiter);
            }

            return actee;
        }
    }
}
