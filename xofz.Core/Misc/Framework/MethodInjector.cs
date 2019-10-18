namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework;

    public class MethodInjector
    {
        public MethodInjector(
            MethodRunner runner)
        {
            this.runner = runner;
            this.methods = new List<Tuple<object, Type, string, string>>(
                0x10000);
        }

        public virtual void Inject<T>(
            Action<T> action,
            string actionName = null,
            string dependencyName = null)
        {
            this.methods.Add(
                Tuple.Create(
                    (object)action, 
                    typeof(T),
                    actionName, 
                    dependencyName));
        }

        public virtual Tuple<Action<T>, T> Request<T>(
            string actionName = null,
            string dependencyName = null)
        {
            var w = this.runner;
            foreach (var tuple in this.methods
                .Where(t =>
                    t.Item2.IsAssignableFrom(typeof(T)) &&
                    t.Item3 == actionName &&
                    t.Item4 == dependencyName))
            {
                var action = tuple.Item1 as Action<T>;
                if (action == default(Action<T>))
                {
                    continue;
                }

                var dependencyExists = false;
                var dependency = default(T);
                w.Run<T>(
                    t =>
                    {
                        dependency = t;
                        dependencyExists = true;
                    },
                    dependencyName);
                if (!dependencyExists)
                {
                    continue;
                }

                return Tuple.Create(action, dependency);
            }

            return Tuple.Create<Action<T>, T>(t => { }, default(T));
        }

        protected readonly MethodRunner runner;
        protected readonly List<Tuple<object, Type, string, string>> methods;
    }
}
