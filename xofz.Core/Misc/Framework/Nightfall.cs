namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class Nightfall
    {
        public Nightfall()
        {
            this.martyr = new Martyr.Martyr();
        }

        public virtual MaterializedEnumerable<object> Process(MaterializedEnumerable<object> references)
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
            this.martyr.BringToGod(new LinkedListMaterializedEnumerable<IDisposable>(lld));

            return new LinkedListMaterializedEnumerable<object>(ll);
        }

        private readonly Martyr.Martyr martyr;
    }
}
