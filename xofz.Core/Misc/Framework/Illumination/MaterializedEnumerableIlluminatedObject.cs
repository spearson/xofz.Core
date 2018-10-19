namespace xofz.Misc.Framework.Illumination
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class LotIlluminatedObject<T> 
        : IlluminatedObject, Lot<T>
    {
        public LotIlluminatedObject(
            Lot<T> lot)
            : base(new object[] { lot })
        {
            this.lot = lot;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.lot.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public long Count => this.lot.Count;

        private readonly Lot<T> lot;
    }
}
