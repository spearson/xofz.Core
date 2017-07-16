namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using System.Linq;
    using Transformation;

    public class Permutator
    {
        public Permutator()
            : this(new EnumerableRotator())
        {
        }

        public Permutator(EnumerableRotator rotator)
        {
            this.rotator = rotator;
        }

        public IEnumerable<MaterializedEnumerable<T>> RequestPermutations<T>(MaterializedEnumerable<T> collection)
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
                    yield return r.Rotate(switched, k);
                }
            }
        }

        private readonly EnumerableRotator rotator;
    }
}
