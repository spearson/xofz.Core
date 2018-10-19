namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class HashSetLotter : Lotter
    {
        Lot<T> Lotter.Materialize<T>(IEnumerable<T> source)
        {
            return new HashSetLot<T>(
                new HashSet<T>(source));
        }
    }
}
