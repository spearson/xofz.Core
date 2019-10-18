namespace xofz.Misc.Framework.Martyr
{
    using System;
    using System.Collections.Generic;
    using xofz.Misc.Framework.Computation;
    using xofz.Misc.Framework.Illumination;

    public struct Martyr
    {
        public Martyr(
            Illuminator illuminator)
        {
            this.imploder = illuminator?.Illumine<Imploder<IDisposable>>(1)
                ?? new Imploder<IDisposable>(1);
        }

        public void BringToGod(
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

        private readonly Imploder<IDisposable> imploder;
    }
}
