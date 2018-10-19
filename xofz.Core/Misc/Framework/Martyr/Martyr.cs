namespace xofz.Misc.Framework.Martyr
{
    using System;
    using xofz.Misc.Framework.Computation;
    using xofz.Misc.Framework.Illumination;

    public struct Martyr
    {
        public Martyr(Illuminator illuminator)
        {
            this.imploder = illuminator.Illumine<Imploder<IDisposable>>(1);
        }

        public void BringToGod(Lot<IDisposable> disposables)
        {
            foreach (var disposable in disposables)
            {
                this.imploder.AddItem(disposable, true);
            }

            this.imploder[0].Dispose();
        }

        private readonly Imploder<IDisposable> imploder;
    }
}
