namespace xofz.Misc.Framework.AI
{
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;

    public class SkippingTreeOrderer<T> : TreeOrderer<T>
    {
        public SkippingTreeOrderer(short numberOfSkips)
        {
            this.numberOfSkips = numberOfSkips;
        }

        public virtual Lot<T> OrderedTree => this.orderedTree;

        public virtual void Order(Tree<T> tree)
        {
            var linkedList = new LinkedList<T>();
            var iterations = (int)((tree.Count / (double)this.numberOfSkips) + 1);
            var subtractor = 0;
            while (subtractor < this.numberOfSkips)
            {
                for (var i = 0; i < iterations; ++i)
                {
                    var amountToSkip = (this.numberOfSkips * (i + 1)) - (subtractor + 1);
                    linkedList.AddLast(tree.Skip(amountToSkip).FirstOrDefault());
                }

                ++subtractor;
            }

            this.orderedTree = new LinkedListLot<T>(linkedList);
        }

        private Lot<T> orderedTree;
        private readonly short numberOfSkips;
    }
}
