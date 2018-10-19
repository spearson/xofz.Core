namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Numerics;

    public class EnumerablePicker
    {
        public virtual IEnumerable<T> Pick<T>(
            IEnumerable<T> source,
            Lot<BigInteger> pickPoints)
        {
            BigInteger counter = 0;
            var enumerator = source.GetEnumerator();
            foreach (var pickPoint in pickPoints)
            {
                while (counter < pickPoint)
                {
                    enumerator.MoveNext();
                    ++counter;
                }

                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }
    }
}
