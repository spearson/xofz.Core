namespace xofz.Framework
{
    using System;
    using System.Reflection;

    public class EventRaiser
    {
        public virtual void Raise(object eventHolder, string eventName, params object[] args)
        {
            var d = (Delegate)eventHolder
                .GetType()
                .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(eventHolder);
            if (d == default(Delegate))
            {
                d = (Delegate)eventHolder
                    .GetType()
                    .BaseType
                    ?.GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(eventHolder);
            }

            d?.DynamicInvoke(args);
        }
    }
}
