namespace xofz.Misc.Framework.Erudition
{
    using System;
    using xofz.Framework.Lots;
    using static EnumerableHelpers;

    public class Likening
    {
        public virtual T Liken<T>(
            Func<T> factory, 
            Lot<Action<T>> acts,
            long limiter)
        {
            var actee = factory();
            FirstOrDefault(
                    acts)
                ?.Invoke(actee);
            var firstHashCode = actee.GetHashCode();
            foreach (var act in Skip(acts, 1))
            {
                act(actee);
                if (Math.Abs(actee.GetHashCode() - firstHashCode) <= limiter)
                {
                    continue;
                }

                return this.Liken(
                    factory,
                    new XLinkedListLot<Action<T>>(
                        XLinkedList<Action<T>>.Create(
                            Except(
                                acts,
                                new[] { act }))),
                    limiter);
            }

            return actee;
        }
    }
}
