namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class Nightfall
    {
        public Nightfall()
        {
            this.martyr = new Martyr.Martyr();
        }

        public virtual Lot<object> Process(Lot<object> references)
        {
            var ll = new LinkedList<object>();
            var lld = new LinkedList<IDisposable>();
            foreach (var reference in references)
            {
                if (reference is IDisposable)
                {
                    lld.AddLast((IDisposable)reference);
                    continue;
                }

                ll.AddLast(reference);
            }

            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            this.martyr.BringToGod(new LinkedListLot<IDisposable>(lld));

            return new LinkedListLot<object>(ll);
        }

        private readonly Martyr.Martyr martyr;
    }
}
