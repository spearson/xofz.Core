namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodWeb
    {
        public static MethodWeb Default
        {
            get
            {
                lock (defaultLocker)
                {
                    if (@default == default(MethodWeb))
                    {
                        // ReSharper disable once ObjectCreationAsStatement
                        // check the constructor
                        new MethodWeb();
                    }
                }

                return @default;
            }

            private set => @default = value;
        }

        public static void SetDefault(Func<MethodWeb> defaultCreator)
        {
            Default = defaultCreator();
        }

        public MethodWeb()
        {
            this.dependencies = new List<Tuple<object, string>>();
            Default = this;
        }

        public virtual void RegisterDependency(
            object dependency, 
            string name = null)
        {
            this.dependencies.Add(
                Tuple.Create(dependency, name));
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

        public virtual Tuple<T, U> Run<T, U>(
            Action<T, U> method = null,
            string dependency1Name = null,
            string dependency2Name = null)
        {
            var ds = this.dependencies;
            var dep1 = ds
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency1Name);
            var dep2 = ds
                .Where(tuple => tuple.Item1 is U)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency2Name);
            if (dep1 == null || dep2 == null)
            {
                return Tuple.Create(
                    default(T),
                    default(U));
            }
            
            var t = (T)dep1.Item1;
            var u = (U)dep2.Item1;
            method?.Invoke(t, u);

            return Tuple.Create(t, u);
        }

        public virtual Tuple<T, U, V> Run<T, U, V>(
            Action<T, U, V> method = null,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null)
        {
            var ds = this.dependencies;
            var dep1 = ds
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency1Name);
            var dep2 = ds
                .Where(tuple => tuple.Item1 is U)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency2Name);
            var dep3 = ds
                .Where(tuple => tuple.Item1 is V)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency3Name);
            if (dep1 == null || dep2 == null || dep3 == null)
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            var t = (T)dep1.Item1;
            var u = (U)dep2.Item1;
            var v = (V)dep3.Item1;
            method?.Invoke(t, u, v);

            return Tuple.Create(t, u, v);
        }

        public virtual Tuple<T, U, V, W> Run<T, U, V, W>(
            Action<T, U, V, W> method = null,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null,
            string dependency4Name = null)
        {
            var ds = this.dependencies;
            var dep1 = ds
                .Where(tuple => tuple.Item1 is T)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency1Name);
            var dep2 = ds
                .Where(tuple => tuple.Item1 is U)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency2Name);
            var dep3 = ds
                .Where(tuple => tuple.Item1 is V)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency3Name);
            var dep4 = ds
                .Where(tuple => tuple.Item1 is W)
                .FirstOrDefault(
                    tuple => tuple.Item2 == dependency4Name);
            if (dep1 == null
                || dep2 == null
                || dep3 == null
                || dep4 == null)
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            var t = (T)dep1.Item1;
            var u = (U)dep2.Item1;
            var v = (V)dep3.Item1;
            var w = (W)dep4.Item1;
            method?.Invoke(t, u, v, w);

            return Tuple.Create(t, u, v, w);
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

        private readonly List<Tuple<object, string>> dependencies;
        private static MethodWeb @default;
        private static readonly object defaultLocker = new object();
    }
}
