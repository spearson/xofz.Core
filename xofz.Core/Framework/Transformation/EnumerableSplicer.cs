namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;
    using Materialization;

    public class EnumerableSplicer
    {
        public MaterializedEnumerable<T> Splice<T>(IEnumerable<T>[] sources)
        {
            var lists = new List<T>[sources.Length];

            // first, enumerate all the items into separate lists
            for (var i = 0; i < sources.Length; ++i)
            {
                lists[i] = new List<T>(new LinkedList<T>(sources[i]));
            }

            // then, splice the lists together
            var list = new List<T>(lists.Sum(l => l.Count));
            var smallestCount = lists.Select(l => l.Count).Min();

            for (var i = 0; i < smallestCount; ++i)
            {
                list.AddRange(lists.Select(l => l[i]));
            }

            var remainingLists = new LinkedList<List<T>>();
            foreach (var l in lists)
            {
                l.RemoveRange(0, smallestCount);
                if (l.Count > 0)
                {
                    remainingLists.AddLast(l);
                }
            }

            if (remainingLists.Count == 0)
            {
                return new OrderedMaterializedEnumerable<T>(list);
            }

            IEnumerable<IEnumerable<T>> remainingEnumerables = remainingLists;
            list.AddRange(this.Splice(remainingEnumerables.ToArray()));

            return new OrderedMaterializedEnumerable<T>(list);
        }
    }
}
