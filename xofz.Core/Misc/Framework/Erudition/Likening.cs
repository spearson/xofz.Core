namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Linq;
    using xofz.Framework.Lots;

    public class Likening
    {
        public virtual T Liken<T>(
            Func<T> factory, 
            Lot<Action<T>> acts,
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
                    new LinkedListLot<Action<T>>(
                        acts.Except(new[] { act })),
                    limiter);
            }

            return actee;
        }
    }
}
