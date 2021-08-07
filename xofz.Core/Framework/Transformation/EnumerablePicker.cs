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
            if (source == null)
            {
                yield break;
            }

            if (pickPoints == null)
            {
                yield break;
            }

            const byte zero = 0;
            BigInteger counter = zero;
            var enumerator = source.GetEnumerator();
            if (enumerator == null)
            {
                yield break;
            }

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
