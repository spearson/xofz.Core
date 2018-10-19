namespace xofz.Misc.Framework.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;

    public class HashCodeSettingTreeOrderer<T> : TreeOrderer<T> where T : MutableHashCode
    {
        public virtual Lot<T> OrderedTree => this.orderedTree; 

        public virtual void Order(Tree<T> tree)
        {
            this.processNodes(tree.Node);

            this.setOrderedTree(
                new LinkedListLot<T>(
                    new LinkedList<T>(
                        tree.OrderBy(t => t.GetHashCode()))));
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

        private void setOrderedTree(Lot<T> orderedTree)
        {
            this.orderedTree = orderedTree;
        }

        private int currentHashCode;
        private Lot<T> orderedTree;
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
