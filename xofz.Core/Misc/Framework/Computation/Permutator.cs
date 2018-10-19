namespace xofz.Misc.Framework.Computation
{
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Transformation;

    public class Permutator
    {
        public Permutator()
            : this(new EnumerableRotatorV2())
        {
        }

        public Permutator(
            EnumerableRotatorV2 rotator)
        {
            this.rotator = rotator;
        }

        public IEnumerable<Lot<T>> RequestPermutations<T>(Lot<T> collection)
        {
            var r = this.rotator;
            for (var i = 0; i < collection.Count; ++i)
            {
                var switched = new LinkedList<T>(
                    collection.Skip(i));
                for (var j = 0; j < i; ++j)
                {
                    switched.AddLast(
                        collection
                        .Skip(j)
                        .FirstOrDefault());
                }

                for (var k = 0; k < i; ++k)
                {
                    yield return r.RotateV2(switched, k);
                }
            }
        }

        private readonly EnumerableRotatorV2 rotator;
    }
}
