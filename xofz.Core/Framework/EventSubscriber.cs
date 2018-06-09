namespace xofz.Framework
{
    using System;

    // this class is pretty much for testing that event subscriptions went through
    // because += is hard to test
    public class EventSubscriber
    {
        public EventSubscriber(MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Subscribe<T>(
            T publisher,
            string eventName,
            Action handler)
        {
            publisher
                .GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U>(
            T publisher,
            string eventName,
            Action<U> handler)
        {
            publisher
                .GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V>(
            T publisher,
            string eventName,
            Action<U, V> handler)
        {
            publisher
                .GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V, W>(
            T publisher,
            string eventName,
            Action<U, V, W> handler)
        {
            publisher
                .GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
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
