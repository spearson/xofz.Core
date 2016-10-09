namespace xofz.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Materialization;

    public class HashCodeSettingTreeOrderer<T> : TreeOrderer<T> where T : MutableHashCode
    {
        public virtual MaterializedEnumerable<T> OrderedTree => this.orderedTree; 

        public virtual void Order(Tree<T> tree)
        {
            this.processNodes(tree.Node);

            this.setOrderedTree(
                new LinkedListMaterializedEnumerable<T>(
                    new LinkedList<T>(tree.OrderBy(t => t.GetHashCode()))));
        }

        private void processNodes(TreeNode<T> headNode)
        {
            this.setCurrentHashCode(this.currentHashCode + 1);
            foreach (var node in headNode.Nodes)
            {
                node.Value.SetHashCode(this.currentHashCode);
                this.processNodes(node);
            }
        }

        private void setCurrentHashCode(int currentHashCode)
        {
            this.currentHashCode = currentHashCode;
        }

        private void setOrderedTree(MaterializedEnumerable<T> orderedTree)
        {
            this.orderedTree = orderedTree;
        }

        private int currentHashCode;
        private MaterializedEnumerable<T> orderedTree;
    }

    public class Tester
    {
        public void Test()
        {
            var tree = new Tree<MutableHashCode>();
            tree.Node.Value = new MutableHashCode();
            for (var i = 0; i < 10; ++i)
            {
                tree.Node.Add(new TreeNode<MutableHashCode>(new MutableHashCode()));
            }

            foreach (var node in tree.Node.Nodes)
            {
                node.Add(new TreeNode<MutableHashCode>(new MutableHashCode()));
                node.Add(new TreeNode<MutableHashCode>(new MutableHashCode()));
                node.Add(new TreeNode<MutableHashCode>(new MutableHashCode()));
            }

            var orderer = new HashCodeSettingTreeOrderer<MutableHashCode>();
            orderer.Order(tree);

            foreach (var item in orderer.OrderedTree)
            {
                Console.WriteLine(item.GetHashCode());
            }
        }
    }
}
