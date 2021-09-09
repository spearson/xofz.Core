namespace xofz.Misc.Framework.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using static EnumerableHelpers;

    public class Tree<T> 
        : Lot<T>
    {
        public Tree() 
            : this(new TreeNode<T>())
        {
        }

        public Tree(
            TreeNode<T> node)
        {
            this.treeNode = node;
        }

        public virtual TreeNode<T> Node => this.treeNode;

        public virtual long Count => this.enumerate(this.treeNode).Count;

        public virtual int ComputeDepth()
        {
            return this.deepen(this.treeNode, 1);
        }

        protected virtual int deepen(
            TreeNode<T> node, 
            int currentDepth)
        {
            if (node.Nodes.Count < 1)
            {
                return currentDepth;
            }

            ++currentDepth;
            var depths = new XLinkedList<int>();
            foreach (var n in node.Nodes)
            {
                depths.AddTail(this.deepen(n, currentDepth));
            }

            return Max(depths);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.enumerate(this.treeNode).GetEnumerator();
        }

        protected virtual IList<T> enumerate(
            TreeNode<T> node)
        {
            var list = new List<T> { node.Value };
            foreach (var n in node.Nodes)
            {
                list.AddRange(this.enumerate(n));
            }

            return list;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected readonly TreeNode<T> treeNode;
    }

    public class TreeNode<T>
    {
        public TreeNode()
            : this(default)
        {
        }

        public TreeNode(
            T value)
        {
            this.value = value;
            this.nodes = new XLinkedList<TreeNode<T>>();
        }

        public virtual T Value
        {
            get => this.value;

            set => this.value = value;
        }

        public virtual Lot<TreeNode<T>> Nodes
            => new XLinkedListLot<TreeNode<T>>(
                this.nodes as XLinkedList<TreeNode<T>>);

        public virtual void Add(
            TreeNode<T> node)
        {
            this.nodes.Add(node);
        }

        public virtual void Clear()
        {
            this.nodes = new XLinkedList<TreeNode<T>>();
        }

        private ICollection<TreeNode<T>> nodes;
        private T value;
    }
}
