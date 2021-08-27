namespace xofz.Misc.Framework.Transcendence
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using xofz.Misc.Framework.Computation;

    public class HolySpirit
    {
        public virtual T Enliven<T>(
            Func<T> factory, 
            IEnumerable<Action<T>> acts,
            BigInteger minimum)
        {
            if (factory == null)
            {
                return default;
            }

            var vc = new VarianceComputer<T>(item => item.GetHashCode());
            var l = new List<Action<T>>(acts);
            var ll = new XLinkedList<Action<T>>();
            var i = factory();
            var original = vc.Compute(actor => { }, i);
            foreach (var act in l)
            {
                if (BigInteger.Abs(original - vc.Compute(act, i)) < minimum)
                {
                    ll.AddTail(act);
                }
            }

            l.RemoveAll(itemToRemove => ll.Contains(itemToRemove));

            var i2 = factory.Invoke();
            foreach (var act in l)
            {
                act(i2);
            }

            return i2;
        }
    }
}
