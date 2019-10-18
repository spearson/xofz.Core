namespace xofz.Misc.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;

    public class HashCodeSettingTreeOrderer<T>
        : TreeOrderer<T> where T : MutableHashCode
    {
        public virtual Lot<T> OrderedTree => this.currentTree; 

        public virtual void Order(
            Tree<T> tree)
        {
            this.processNodes(tree.Node);

            this.setCurrentTree(
                new LinkedListLot<T>(
                    new LinkedList<T>(
                        tree.OrderBy(t => t.GetHashCode()))));
        }

        protected virtual void processNodes(
            TreeNode<T> headNode)
        {
            this.setCurrentHashCode(
                this.currentHashCode + 1);
            foreach (var node in headNode.Nodes)
            {
                node.Value.SetHashCode(this.currentHashCode);
                this.processNodes(node);
            }
        }

        protected virtual void setCurrentHashCode(
            int currentHashCode)
        {
            this.currentHashCode = currentHashCode;
        }

        protected virtual void setCurrentTree(
            Lot<T> currentTree)
        {
            this.currentTree = currentTree;
        }

        protected int currentHashCode;
        protected Lot<T> currentTree;
    }
}
