namespace xofz.Misc.Framework.Martyr
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework;
    using xofz.Misc.Framework.Computation;
    using xofz.Misc.Framework.Illumination;

    public class Martyr
    {
        public Martyr()
            : this(new Illuminator())
        {
        }

        public Martyr(
            Illuminator illuminator)
            : this(illuminator
                ?.Illumine<Imploder<IDisposable>>(
                    defaultCapacity))
        {
        }

        public Martyr(
            Factory factory)
            : this(factory?.Create<Imploder<IDisposable>>())
        {
        }

        public Martyr(
            Imploder<IDisposable> imploder)
        {
            this.imploder = imploder ?? new Imploder<IDisposable>(
                defaultCapacity);
        }

        public virtual void BringToGod(
            IEnumerable<IDisposable> disposables)
        {
            if (disposables == null)
            {
                return;
            }

            foreach (var disposable in disposables)
            {
                this.imploder.AddItem(disposable, true);
            }

            this.imploder[0].Dispose();
        }

        protected readonly Imploder<IDisposable> imploder;
        protected const int defaultCapacity = 1;
    }
}
