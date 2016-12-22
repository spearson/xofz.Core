namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new List<Tuple<object, string>>();
        }

        public virtual void RegisterDependency(
            object dependency, 
            string name = null)
        {
            this.dependencies.Add(
                Tuple.Create(dependency, name));
        }

        public virtual void Subscribe<T>(
            string eventName, 
            Action eventHandler, 
            string dependencyName = null)
        {
            this.subscribeInternal<T>(
                eventName,
                eventHandler,
                dependencyName);
        }

        public virtual void Subscribe<T, U>(
            string eventName,
            Action<U> eventHandler,
            string dependencyName = null)
        {
            this.subscribeInternal<T>(
                eventName,
                eventHandler,
                dependencyName);
        }

        private void subscribeInternal<T>(
            string eventName,
            Delegate eventHandler,
            string dependencyName = null)
        {
            var dependency = this.dependencies
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(tuple => tuple.Item2 == dependencyName);
            if (dependency == null)
            {
                return;
            }

            var e = dependency.Item1
                .GetType()
                .GetEvent(eventName);
            e.AddEventHandler(
                dependency.Item1,
                eventHandler);
        }

        public virtual void Unsubscribe<T>(
            string eventName, 
            Action eventHandler,
            string dependencyName = null)
        {
            this.unsubscribeInternal<T>(
                eventName,
                eventHandler,
                dependencyName);
        }

        public virtual void Unsubscribe<T, U>(
            string eventName,
            Action<U> eventHandler,
            string dependencyName = null)
        {
            this.unsubscribeInternal<T>(
                eventName,
                eventHandler,
                dependencyName);
        }

        private void unsubscribeInternal<T>(
            string eventName,
            Delegate eventHandler,
            string dependencyName = null)
        {
            var dependency = this.dependencies
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(tuple => tuple.Item2 == dependencyName);
            if (dependency == null)
            {
                return;
            }

            var e = dependency.Item1
                .GetType()
                .GetEvent(eventName);
            e.RemoveEventHandler(
                dependency.Item1,
                eventHandler);
        }

        public virtual U Run<T, U>(
            Func<T, U> method,
            string dependencyName = null)
        {
            var dependency = this.dependencies
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(tuple => tuple.Item2 == dependencyName);
            if (dependency == null)
            {
                return default(U);
            }

            return method((T)dependency.Item1);
        }

        public virtual T Run<T>(
            Action<T> method = null,
            string dependencyName = null)
        {
            var dependency = this.dependencies
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(tuple => tuple.Item2 == dependencyName);
            if (dependency == null)
            {
                return default(T);
            }

            var t = (T)dependency.Item1;
            method?.Invoke(t);

            return t;
        }

        private readonly List<Tuple<object, string>> dependencies;
    }
}
