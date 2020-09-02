namespace xofz.Misc.Framework.AI
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using static EnumerableHelpers;

    public class SkippingTreeOrderer<T> : TreeOrderer<T>
    {
        public SkippingTreeOrderer(
            short numberOfSkips)
        {
            this.numberOfSkips = numberOfSkips;
        }

        public virtual Lot<T> OrderedTree => this.currentTree;

        public virtual void Order(
            Tree<T> tree)
        {
            var linkedList = new LinkedList<T>();
            var iterations = (int)(tree.Count / (double)this.numberOfSkips + 1);
            var subtractor = 0;
            while (subtractor < this.numberOfSkips)
            {
                for (var i = 0; i < iterations; ++i)
                {
                    var amountToSkip = (this.numberOfSkips * (i + 1)) -
                                       (subtractor + 1);
                    linkedList.AddLast(
                        FirstOrDefault(
                            Skip(
                                tree,
                                amountToSkip)));
                }

                ++subtractor;
            }

            this.currentTree = new LinkedListLot<T>(linkedList);
        }

        protected Lot<T> currentTree;
        protected readonly short numberOfSkips;
    }
}
