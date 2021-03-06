namespace xofz.Misc.Framework.Conditionality
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class Evaluator
    {
        public virtual bool Evaluate(
            IEnumerable<Func<bool>> conditionGenerators, 
            BigInteger numberToCheck)
        {
            if (conditionGenerators == null)
            {
                return false;
            }

            var enumerator = conditionGenerators.GetEnumerator();
            bool evaluation;
            BigInteger counter = 0;

            while (counter < numberToCheck)
            {
                enumerator.MoveNext();
                evaluation = enumerator.Current?.Invoke() ?? true;
                if (!evaluation)
                {
                    enumerator.Dispose();
                    return false;
                }

                ++counter;
            }

            enumerator.Dispose();
            return true;
        }

        public virtual bool Evaluate<T>(
            IEnumerable<Func<T, bool>> conditions, 
            T actor, 
            BigInteger numberToCheck)
        {
            if (conditions == null)
            {
                return false;
            }

            var enumerator = conditions.GetEnumerator();
            bool evaluation;
            BigInteger counter = 0;
            while (counter < numberToCheck)
            {
                enumerator.MoveNext();
                evaluation = enumerator.Current
                    ?.Invoke(actor) ?? true;
                if (!evaluation)
                {
                    enumerator.Dispose();
                    return false;
                }

                ++counter;
            }

            enumerator.Dispose();
            return true;
        }
    }
}
