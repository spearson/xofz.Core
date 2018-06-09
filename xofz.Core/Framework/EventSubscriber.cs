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
                ?.GetType()
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
                ?.GetType()
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
                ?.GetType()
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
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T>(
            T publisher,
            string eventName,
            Action handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U>(
            T publisher,
            string eventName,
            Action<U> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V>(
            T publisher,
            string eventName,
            Action<U, V> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V, W>(
            T publisher,
            string eventName,
            Action<U, V, W> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        private readonly MethodWeb web;
    }
}
