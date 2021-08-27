namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class Nightfall
    {
        public Nightfall()
            : this(new Martyr.Martyr())
        {
        }

        public Nightfall(
            Martyr.Martyr martyr)
        {
            this.martyr = martyr;
        }

        public virtual Lot<object> Process(
            IEnumerable<object> references)
        {
            var ll = new XLinkedList<object>();
            var lld = new XLinkedList<IDisposable>();
            foreach (var reference in references)
            {
                if (reference is IDisposable)
                {
                    lld.AddTail((IDisposable)reference);
                    continue;
                }

                ll.AddTail(reference);
            }

            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            this.martyr.BringToGod(new LinkedListLot<IDisposable>(lld));

            return new LinkedListLot<object>(ll);
        }

        protected readonly Martyr.Martyr martyr;
    }
}
