namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = xofz.EnumerableHelpers;

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
                var sequence = EH.Take(
                    remainingItems, slicePoint);
                array[counter] = new LinkedListLot<T>(sequence);
                ++counter;
                remainingItems = new LinkedList<T>(
                    EH.Skip(
                        remainingItems,
                        slicePoint));
            }

            return array;
        }
    }
}
