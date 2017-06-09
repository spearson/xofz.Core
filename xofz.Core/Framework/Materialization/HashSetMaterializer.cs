namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class HashSetMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new HashSetMaterializedEnumerable<T>(
                new HashSet<T>(source));
        }
    }
}
