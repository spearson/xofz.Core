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
            if (source == null)
            {
                return new Lot<T>[0];
            }

            if (slicePoints == null || slicePoints.Count < 1)
            {
                return new Lot<T>[0];
            }

            ICollection<T> remainingItems 
                = new LinkedList<T>(source);
            var array = new Lot<T>[slicePoints.Count];
            var counter = 0;
            foreach (var slicePoint in slicePoints)
            {
                var sequence = remainingItems.Take(slicePoint);
                array[counter] = new LinkedListLot<T>(sequence);
                ++counter;
                remainingItems = new LinkedList<T>(
                    remainingItems.Skip(slicePoint));
            }

            return array;
        }
    }
}
