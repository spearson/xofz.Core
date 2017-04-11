namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableRaker
    {
        public virtual IEnumerable<T> Rake<T>(
            IEnumerable<T> source,
            IEnumerable<int> passPoints)
        {
            var e = source.GetEnumerator();
            var counter = 0;
            foreach (var passPoint in passPoints)
            {
                while (counter < passPoint)
                {
                    e.MoveNext();
                    ++counter;
                }

                yield return e.Current;
                counter = 0;
            }

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            e.Dispose();
        }
    }
}
