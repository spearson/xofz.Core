namespace xofz.Misc.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using xofz.Framework.Transformation;
    using static EnumerableHelpers;

    public class Reorderer<T>
    {
        public Reorderer(
            EnumerableRotator rotator,
            EnumerableConnector connector)
        {
            this.rotator = rotator 
                           ?? new EnumerableRotator();
            this.connector = connector 
                             ?? new EnumerableConnector();
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
            var ll = XLinkedList<T>.Create(source);
            var rotated = this.rotator.Rotate(
                ll,
                startIndex,
                false);

            var start = Take(rotated, take);
            var next = Take(ll, startIndex);
            var end = Empty<T>();
            if (ll.Count > startIndex + take)
            {
                end = Skip(ll, startIndex + take);
            }

            return this.connector.Connect(start, next, end);
        }

        protected readonly EnumerableRotator rotator;
        protected readonly EnumerableConnector connector;
    }
}
