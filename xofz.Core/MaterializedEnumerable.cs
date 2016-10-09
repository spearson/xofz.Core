namespace xofz
{
    using System.Collections.Generic;

    public interface MaterializedEnumerable<out T> : IEnumerable<T>
    {
        long Count { get; }
    }
}
