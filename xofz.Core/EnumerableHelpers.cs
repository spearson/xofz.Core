namespace xofz
{
    using System.Collections.Generic;

    public class EnumerableHelpers
    {
        public static IEnumerable<T> Iterate<T>(params T[] items)
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}
