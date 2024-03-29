﻿namespace xofz.Framework
{
    using System;

    public class EventSubscriberV2 
        : EventSubscriber
    {
        public virtual void Subscribe(
            object publisher,
            string eventName,
            Action handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T>(
            object publisher,
            string eventName,
            Action<T> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U>(
            object publisher,
            string eventName,
            Action<T, U> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V>(
            object publisher,
            string eventName,
            Action<T, U, V> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V, W>(
            object publisher,
            string eventName,
            Action<T, U, V, W> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            Action handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T>(
            object publisher,
            string eventName,
            Action<T> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U>(
            object publisher,
            string eventName,
            Action<T, U> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V>(
            object publisher,
            string eventName,
            Action<T, U, V> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V, W>(
            object publisher,
            string eventName,
            Action<T, U, V, W> handler)
        {
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }
    }
}
