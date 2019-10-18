namespace xofz.Misc.Framework.Theory
{
    using System;
    using System.Collections.Generic;

    public class ActionableTransaction<T>
    {
        public ActionableTransaction()
            : this(Activator.CreateInstance<T>)
        {
        }

        public ActionableTransaction(
            Func<T> defaultFactory)
        {
            this.currentFactory = defaultFactory;
        }

        public virtual Func<T> Factory
        {
            get => this.currentFactory;

            set => this.currentFactory = value;
        }

        public virtual T Transact(
            IEnumerable<Action<T>> actions)
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
                    return this.Factory();
                }
            }

            return t;
        }

        protected Func<T> currentFactory;
    }
}
