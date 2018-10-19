namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;

    public class EnumerableSlicer
    {
        public virtual Lot<T>[] Slice<T>(
            IEnumerable<T> source,
            Lot<int> slicePoints)
        {
            var ll = new LinkedList<T>(source);
            var array = new Lot<T>[slicePoints.Count];
            var counter = 0;
            foreach (var slicePoint in slicePoints)
            {
                var sequence = ll.Take(slicePoint);
                array[counter] =
                    new LinkedListLot<T>(sequence);
                ++counter;
                ll = new LinkedList<T>(ll.Skip(slicePoint));
            }

            return array;
        }
    }
}
