namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Parser
    {
        public virtual IEnumerable<Action<T>> ParseLearningMethods<T>(
            Func<T> factory,
            IEnumerable<Action<T>> methodsToParse)
        {
            foreach (var method in methodsToParse)
            {
                var item = factory();
                var hashCode = item.GetHashCode();
                method(item);
                if (item.GetHashCode() == hashCode)
                {
                    yield return method;
                }
            }
        }
    }
}
