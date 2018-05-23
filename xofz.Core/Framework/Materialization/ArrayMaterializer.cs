namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ArrayMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new ArrayMaterializedEnumerable<T>(
                source.ToArray());
        }
    }
}