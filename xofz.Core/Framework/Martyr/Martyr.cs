namespace xofz.Framework.Martyr
{
    using System;
    using Computation;
    using Illumination;

    public struct Martyr
    {
        public Martyr(Illuminator illuminator)
        {
            this.imploder = illuminator.Illumine<Imploder<IDisposable>>(1);
        }

        public void BringToGod(MaterializedEnumerable<IDisposable> disposables)
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
