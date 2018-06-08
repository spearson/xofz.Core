namespace xofz.Framework
{
    using System;

    public class EventSubscriber
    {
        public EventSubscriber(MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Subscribe<T>(
            Func<T, string> readEventName,
            Action handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.AddEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Subscribe<T, U>(
            Func<T, string> readEventName,
            Action<U> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.AddEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Subscribe<T, U, V>(
            Func<T, string> readEventName,
            Action<U, V> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.AddEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Subscribe<T, U, V, W>(
            Func<T, string> readEventName,
            Action<U, V, W> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.AddEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Unsubscribe<T>(
            Func<T, string> readEventName,
            Action handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.RemoveEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Unsubscribe<T, U>(
            Func<T, string> readEventName,
            Action<U> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.RemoveEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Unsubscribe<T, U, V>(
            Func<T, string> readEventName,
            Action<U, V> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.RemoveEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        public virtual void Unsubscribe<T, U, V, W>(
            Func<T, string> readEventName,
            Action<U, V, W> handler,
            string dependencyName = null)
        {
            var w = this.web;
            w.Run<T>(t =>
                {
                    t
                        .GetType()
                        .GetEvent(readEventName(t))
                        ?.RemoveEventHandler(
                            t,
                            handler);
                },
                dependencyName);
        }

        private readonly MethodWeb web;
    }
}
