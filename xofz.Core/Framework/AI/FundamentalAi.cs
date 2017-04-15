namespace xofz.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using Materialization;

    public class FundamentalAi<T>
    {
        public FundamentalAi(TreeOrderer<T> treeOrderer)
        {
            this.treeOrderer = treeOrderer;
        }

        public virtual MaterializedEnumerable<T> Act(Tree<T> tree, IEnumerable<Action<T>> actions)
        {
            this.treeOrderer.Order(tree);
            var actionEnumerator = actions.GetEnumerator();
            var linkedList = new LinkedList<T>();
            foreach (var value in this.treeOrderer.OrderedTree)
            {
                actionEnumerator.MoveNext();
                actionEnumerator.Current(value);
                linkedList.AddLast(value);
            }

            actionEnumerator.Dispose();
            return new LinkedListMaterializedEnumerable<T>(linkedList);
        }

        private readonly TreeOrderer<T> treeOrderer;
    }
}
