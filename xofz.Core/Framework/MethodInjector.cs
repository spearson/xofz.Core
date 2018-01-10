namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodInjector
    {
        public MethodInjector(MethodWeb web)
        {
            this.web = web;
            this.methods = new List<Tuple<object, string, string>>(
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
                    actionName, 
                    dependencyName));
        }

        public virtual Tuple<Action<T>, T> Request<T>(
            string actionName = null,
            string dependencyName = null)
        {
            var w = this.web;
            foreach (var tuple in this.methods
                .Where(t => t.Item2 == actionName 
                            && t.Item3 == dependencyName))
            {
                var action = tuple.Item1 as Action<T>;
                if (action == default(Action<T>))
                {
                    continue;
                }

                var dependency = w.Run<T>(
                    t => { },
                    dependencyName);
                if (dependency.Equals(default(T)))
                {
                    continue;
                }

                return Tuple.Create(action, dependency);
            }

            return Tuple.Create<Action<T>, T>(t => { }, default(T));
        }

        private readonly MethodWeb web;
        private readonly List<Tuple<object, string, string>> methods;
    }
}
