namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new List<object>();
        }

        public virtual void RegisterDependency(object dependency)
        {
            this.dependencies.Add(dependency);
        }

        public virtual U Run<T, U>(Func<T, U> method)
        {
            var dependency = this.dependencies.FirstOrDefault(d => d is T);
            if (dependency == null)
            {
                return default(U);
            }

            return method((T)dependency);
        }

        public virtual T Run<T>(Action<T> method)
        {
            var dependency = this.dependencies.FirstOrDefault(d => d is T);
            if (dependency == null)
            {
                return default(T);
            }

            var t = (T)dependency;
            method(t);

            return t;
        }

        private readonly List<object> dependencies;
    }
}
