namespace xofz.Framework.Theory
{
    using System;
    using System.Collections.Generic;

    public class ActionableTransaction<T>
    {
        public ActionableTransaction(Func<T> factory)
        {
            this.factory = factory;
        }

        public virtual T Transact(IEnumerable<Action<T>> actions)
        {
            var t = this.factory();
            foreach (var action in actions)
            {
                try
                {
                    action(t);
                }
                catch
                {
                    return this.factory();
                }
            }

            return t;
        }

        private readonly Func<T> factory;
    }
}
