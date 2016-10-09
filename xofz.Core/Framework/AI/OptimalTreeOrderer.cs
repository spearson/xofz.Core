namespace xofz.Framework.AI
{
    using System.Collections.Generic;
    using Materialization;

    public class OptimalTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual MaterializedEnumerable<T> OrderedTree => this.orderedTree;

        public virtual void Order(Tree<T> tree)
        {
            // warning: not thread safe
            this.primaryLinkedList = new LinkedList<T>();
            this.secondaryLinkedList = new LinkedList<T>();
            this.depth = tree.ComputeDepth();
            this.checkNode(tree.Node);
            var list = new List<T>();
            list.AddRange(this.primaryLinkedList);
            list.AddRange(this.secondaryLinkedList);
            this.orderedTree = new OrderedMaterializedEnumerable<T>(list);
        }

        private void checkNode(TreeNode<T> node)
        {
            if (node.Nodes.Count > this.depth)
            {
                this.primaryLinkedList.AddLast(node.Value);
            }
            else
            {
                this.secondaryLinkedList.AddLast(node.Value);
            }

            foreach (var n in node.Nodes)
            {
                this.checkNode(n);
            }
        }

        private int depth;
        private LinkedList<T> primaryLinkedList, secondaryLinkedList;
        private MaterializedEnumerable<T> orderedTree;
    }
}
