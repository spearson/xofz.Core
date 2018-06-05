namespace xofz.Misc.Framework.Theory
{
    using System;
    using System.Collections.Generic;

    public class ActionableTransaction<T>
    {
        public ActionableTransaction()
        {
            this.factory = () => Activator.CreateInstance<T>();
        }

        public ActionableTransaction(
            Func<T> defaultFactory)
        {
            this.factory = defaultFactory;
        }

        public virtual Func<T> Factory
        {
            get => this.factory;

            set => this.factory = value;
        }

        public virtual T Transact(IEnumerable<Action<T>> actions)
        {
            var t = this.Factory();
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

        private Func<T> factory;
    }
}
