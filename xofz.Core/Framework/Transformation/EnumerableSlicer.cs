namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = xofz.EnumerableHelpers;

    public class EnumerableSlicer
    {
        public virtual Lot<T>[] Slice<T>(
            IEnumerable<T> finiteSource,
            Lot<int> slicePoints)
        {
            const byte 
                zero = 0,
                one = 1;
            if (finiteSource == null)
            {
                return new Lot<T>[zero];
            }

            if (slicePoints == null || slicePoints.Count < one)
            {
                return new Lot<T>[zero];
            }

            ICollection<T> remainingItems
                = XLinkedList<T>.Create(finiteSource);
            var array = new Lot<T>[slicePoints.Count];
            int counter = zero;
            foreach (var slicePoint in slicePoints)
            {
                var sequence = EH.Take(
                    remainingItems, 
                    slicePoint);
                array[counter] = new XLinkedListLot<T>(
                    XLinkedList<T>.Create(
                        sequence));
                ++counter;
                remainingItems = XLinkedList<T>.Create(
                    EH.Skip(
                        remainingItems,
                        slicePoint));
            }

            return array;
        }
    }
}
