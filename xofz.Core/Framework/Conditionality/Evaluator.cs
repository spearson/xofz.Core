namespace xofz.Framework.Conditionality
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
            var enumerator = conditionGenerators.GetEnumerator();
            bool evaluation;
            BigInteger counter = 0;
            while (counter < numberToCheck)
            {
                enumerator.MoveNext();
                evaluation = enumerator.Current();
                if (!evaluation)
                {
                    return false;
                }

                ++counter;
            }

            return true;
        }

        public virtual bool Evaluate<T>(
            IEnumerable<Func<T, bool>> conditions, 
            T actor, 
            BigInteger numberToCheck)
        {
            var enumerator = conditions.GetEnumerator();
            bool evaluation;
            BigInteger counter = 0;
            while (counter < numberToCheck)
            {
                enumerator.MoveNext();
                evaluation = enumerator.Current(actor);
                if (!evaluation)
                {
                    return false;
                }

                ++counter;
            }

            return true;
        }
    }
}
