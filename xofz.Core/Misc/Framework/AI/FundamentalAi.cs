namespace xofz.Misc.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class FundamentalAi<T>
    {
        public FundamentalAi(
            TreeOrderer<T> treeOrderer)
        {
            this.treeOrderer = treeOrderer;
        }

        public virtual Lot<T> Act(
            Tree<T> tree, 
            IEnumerable<Action<T>> actions)
        {
            var orderer = this.treeOrderer;
            if (actions == null)
            {
                orderer.Order(tree);
                return orderer.OrderedTree;
            }

            orderer.Order(tree);
            var actionEnumerator = actions.GetEnumerator();
            var linkedList = new LinkedList<T>();
            foreach (var value in this.treeOrderer.OrderedTree)
            {
                actionEnumerator.MoveNext();
                actionEnumerator.Current?.Invoke(value);
                linkedList.AddLast(value);
            }

            actionEnumerator.Dispose();
            return new LinkedListLot<T>(linkedList);
        }

        protected readonly TreeOrderer<T> treeOrderer;
    }
}
