namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;
    
    public class Reorderer<T>
    {
        public Reorderer(
            EnumerableRotator rotator,
            EnumerableConnector connector)
        {
            this.rotator = rotator;
            this.connector = connector;
        }

        public virtual Lot<T> Reorder(
            IEnumerable<T> source,
            int startIndex)
        {
            return this.rotator.Rotate(
                source,
                startIndex,
                false);
        }

        public virtual Lot<T> Reorder(
            IEnumerable<T> source,
            int startIndex,
            int take)
        {
            var ll = new LinkedList<T>(source);
            var rotated = this.rotator.Rotate(
                ll,
                startIndex,
                false);

            var start = rotated.Take(take);
            var next = ll.Take(startIndex);
            var end = Enumerable.Empty<T>();
            if (ll.Count > startIndex + take)
            {
                end = ll.Skip(startIndex + take);
            }

            return new LinkedListLot<T>(
                this.connector.Connect(start, next, end));
        }

        private readonly EnumerableRotator rotator;
        private readonly EnumerableConnector connector;
    }
}
