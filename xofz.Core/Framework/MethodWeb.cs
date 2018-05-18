namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new LinkedList<Tuple<object, string>>();
        }

        public virtual void RegisterDependency(
            object dependency, 
            string name = null)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(
                    nameof(dependency));
            }

            this.dependencies.AddLast(
                Tuple.Create(dependency, name));
        }

        public virtual T Run<T>(
            Action<T> method = null,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            Tuple<object, string> dependency;
            try
            {
                dependency = ds
                    .Where(tuple => tuple.Item1 is T)
                    .First(tuple => tuple.Item2 == dependencyName);
            }
            catch
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
            Tuple<object, string> dep1;
            Tuple<object, string> dep2;
            try
            {
                dep1 = ds
                    .Where(tuple => tuple.Item1 is T)
                    .First(tuple => tuple.Item2 == dependency1Name);
                dep2 = ds
                    .Where(tuple => tuple.Item1 is U)
                    .First(tuple => tuple.Item2 == dependency2Name);
            }
            catch
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
            Tuple<object, string> dep1;
            Tuple<object, string> dep2;
            Tuple<object, string> dep3;
            try
            {
                dep1 = ds
                    .Where(tuple => tuple.Item1 is T)
                    .First(tuple => tuple.Item2 == dependency1Name);
                dep2 = ds
                    .Where(tuple => tuple.Item1 is U)
                    .First(tuple => tuple.Item2 == dependency2Name);
                dep3 = ds
                    .Where(tuple => tuple.Item1 is V)
                    .First(tuple => tuple.Item2 == dependency3Name);
            }
            catch
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
            Tuple<object, string> dep1;
            Tuple<object, string> dep2;
            Tuple<object, string> dep3;
            Tuple<object, string> dep4;
            try
            {
                dep1 = ds
                    .Where(tuple => tuple.Item1 is T)
                    .First(tuple => tuple.Item2 == dependency1Name);
                dep2 = ds
                    .Where(tuple => tuple.Item1 is U)
                    .First(tuple => tuple.Item2 == dependency2Name);
                dep3 = ds
                    .Where(tuple => tuple.Item1 is V)
                    .First(tuple => tuple.Item2 == dependency3Name);
                dep4 = ds
                    .Where(tuple => tuple.Item1 is W)
                    .First(tuple => tuple.Item2 == dependency4Name);
            }
            catch
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

        private readonly LinkedList<Tuple<object, string>> dependencies;
    }
}
