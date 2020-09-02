namespace xofz.Misc.Framework.Computation
{
    using System.Collections.Generic;
    using xofz.Framework.Transformation;
    using static EnumerableHelpers;

    public class Permutator
    {
        public Permutator()
            : this(new EnumerableRotator())
        {
        }

        public Permutator(
            EnumerableRotator rotator)
        {
            this.rotator = rotator;
        }

        public virtual IEnumerable<Lot<T>> RequestPermutations<T>(
            Lot<T> collection)
        {
            var r = this.rotator;
            for (var i = 0; i < collection.Count; ++i)
            {
                var switched = new LinkedList<T>(
                    Skip(
                        collection,
                        i));
                for (var j = 0; j < i; ++j)
                {
                    switched.AddLast(
                        FirstOrDefault(
                            Skip(
                                collection,
                                j)));
                }

                for (var k = 0; k < i; ++k)
                {
                    yield return r.Rotate(switched, k);
                }
            }
        }

        protected readonly EnumerableRotator rotator;
    }
}
