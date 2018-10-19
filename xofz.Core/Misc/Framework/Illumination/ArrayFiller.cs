namespace xofz.Misc.Framework.Illumination
{
    public class ArrayFiller
    {
        public ArrayFiller(Illuminator illuminator)
        {
            this.illuminator = illuminator;
        }

        public virtual T[] Fill<T>(T[] array, Lot<object> dependencies)
            where T : class
        {
            for (var i = 0; i < array.Length; ++i)
            {
                var t = this.illuminator.Illumine<T>(dependencies);
                array[i] = t;
            }

            return array;
        }

        private readonly Illuminator illuminator;
    }
}
