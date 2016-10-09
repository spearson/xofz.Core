namespace xofz.Framework.Erudition
{
    using System;

    public class Absolution<T>
    {
        public Absolution(Learner<T> learner1, Learner<T> learner2)
        {
            this.learner1 = learner1;
            this.learner2 = learner2;
        }

        public virtual Tuple<T, T> Absolve(Func<T> factory, Action<T> safeMethod, bool oneBeforeTwo = true)
        {
            var l1 = this.learner1;
            var l2 = this.learner2;
            T item1, item2;
            if (oneBeforeTwo)
            {
                l1.Learn(factory, safeMethod);
                item1 = l1.Request;
                l2.Learn(factory, safeMethod);
                item2 = l2.Request;
                return Tuple.Create(item1, item2);
            }

            l2.Learn(factory, safeMethod);
            item1 = l2.Request;
            l1.Learn(factory, safeMethod);
            item2 = l1.Request;
            return Tuple.Create(item1, item2);
        }

        private readonly Learner<T> learner1, learner2;
    }
}
