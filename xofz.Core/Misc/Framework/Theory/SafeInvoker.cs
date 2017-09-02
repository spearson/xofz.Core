namespace xofz.Misc.Framework.Theory
{
    using System;
    using System.Collections.Generic;

    public class SafeInvoker
    {
        public virtual IEnumerable<Action> SafeInvoke(IEnumerable<Action> suspiciousActions)
        {
            foreach (var action in suspiciousActions)
            {
                bool safe;
                try
                {
                    action();
                    safe = true;
                }
                catch
                {
                    safe = false;
                }

                yield return safe ? action : () => { };
            }
        }
    }
}
