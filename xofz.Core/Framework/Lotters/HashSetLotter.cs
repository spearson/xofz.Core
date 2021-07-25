namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class HashSetLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return new HashSetLot<T>();
            }

            return new HashSetLot<T>(
                new HashSet<T>(source));
        }

        public ICollection<T> Collect<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return new HashSet<T>();
            }

            return new HashSet<T>(
                source);
        }
    }
}
