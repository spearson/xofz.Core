namespace xofz.Misc.Framework.AI
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class OptimalTreeOrderer<T> 
        : TreeOrderer<T>
    {
        public virtual Lot<T> OrderedTree => this.currentTree;

        public virtual void Order(
            Tree<T> tree)
        {
            // warning: not thread safe
            this.primaryLinkedList = new XLinkedList<T>();
            this.secondaryLinkedList = new XLinkedList<T>();
            this.currentTreeDepth = tree.ComputeDepth();
            this.checkNode(tree.Node);
            var list = new List<T>();
            list.AddRange(this.primaryLinkedList);
            list.AddRange(this.secondaryLinkedList);
            this.currentTree = new ListLot<T>(list);
        }

        protected virtual void checkNode(
            TreeNode<T> node)
        {
            if (node.Nodes.Count > this.currentTreeDepth)
            {
                this.primaryLinkedList.AddTail(node.Value);
                goto checkSubNodes;
            }

            this.secondaryLinkedList.AddTail(node.Value);

            checkSubNodes:
            foreach (var n in node.Nodes)
            {
                this.checkNode(n);
            }
        }

        protected int currentTreeDepth;
        protected XLinkedList<T> primaryLinkedList, secondaryLinkedList;
        protected Lot<T> currentTree;
    }
}
