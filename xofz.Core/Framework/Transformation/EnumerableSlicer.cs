namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Materialization;

    public class EnumerableSlicer
    {
        public virtual MaterializedEnumerable<T>[] Slice<T>(
            IEnumerable<T> source,
            MaterializedEnumerable<int> slicePoints)
        {
            var ll = new LinkedList<T>(source);
            var array = new MaterializedEnumerable<T>[slicePoints.Count];
            var counter = 0;
            foreach (var slicePoint in slicePoints)
            {
                var sequence = ll.Take(slicePoint);
                array[counter] =
                    new LinkedListMaterializedEnumerable<T>(sequence);
                ++counter;
                ll = new LinkedList<T>(ll.Skip(slicePoint));
            }

            return array;
        }
    }
}
