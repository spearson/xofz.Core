namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodWebManager
    {
        public MethodWebManager()
        {
            this.webs = new LinkedList<NamedMethodWebHolder>();
        }

        public virtual IEnumerable<string> WebNames()
        {
            return this.webs.Select(nmwh => nmwh.Name);
        }

        public virtual void AddWeb(MethodWeb web, string name = @"Default")
        {
            if (web == null)
            {
                throw new InvalidOperationException(
                    "Cannot add a null web.");
            }

            var ws = this.webs;
            var namedWeb = ws
                .FirstOrDefault(nmwh => ReferenceEquals(web, nmwh.Web));
            if (namedWeb != default(NamedMethodWebHolder))
            {
                throw new InvalidOperationException(
                    "That web has already been added with name: "
                    + namedWeb.Name);
            }

            if (ws
                .Select(nmwh => nmwh.Name)
                .Contains(name))
            {
                throw new InvalidOperationException(
                    "Name \"" + name + "\" is already taken.");
            }

            ws.AddLast(
                new NamedMethodWebHolder
                {
                    Web = web,
                    Name = name
                });
        }

        public virtual void AccessWeb(
            Action<MethodWeb> accessor,
            string webName)
        {
            var w = this.webs.FirstOrDefault(
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return;
            }

            accessor(w.Web);
        }

        public virtual T RunWeb<T>(
            Action<T> engine, 
            string webName, 
            string dependencyName = null)
        {
            var w = this.webs.FirstOrDefault(
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return default(T);
            }

            return w.Web.Run(engine, dependencyName);
        }

        public virtual Tuple<T, U> RunWeb<T, U>(
            Action<T, U> engine,
            string webName,
            string dependency1Name = null,
            string dependency2Name = null)
        {
            var w = this.webs.FirstOrDefault(
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
                    default(T), 
                    default(U));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name);
        }

        public virtual Tuple<T, U, V> RunWeb<T, U, V>(
            Action<T, U, V> engine,
            string webName,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null)
        {
            var w = this.webs.FirstOrDefault(
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name,
                dependency3Name);
        }

        public virtual Tuple<T, U, V, W> RunWeb<T, U, V, W>(
            Action<T, U, V, W> engine,
            string webName,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null,
            string dependency4Name = null)
        {
            var w = this.webs.FirstOrDefault(
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name,
                dependency3Name,
                dependency4Name);
        }

        private readonly LinkedList<NamedMethodWebHolder> webs;

        private class NamedMethodWebHolder
        {
            public virtual MethodWeb Web { get; set; }

            public virtual string Name { get; set; }
        }
    }
}
