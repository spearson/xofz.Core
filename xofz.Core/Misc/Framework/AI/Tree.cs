namespace xofz.Misc.Framework.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Lots;

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
            var depths = new LinkedList<int>();
            foreach (var n in node.Nodes)
            {
                depths.AddLast(this.deepen(n, currentDepth));
            }

            return depths.Max();
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
            this.nodes = new LinkedList<TreeNode<T>>();
        }

        public virtual T Value
        {
            get => this.value;

            set => this.value = value;
        }

        public virtual Lot<TreeNode<T>> Nodes
            => new LinkedListLot<TreeNode<T>>(this.nodes);

        public virtual void Add(TreeNode<T> node)
        {
            this.nodes.AddLast(node);
        }

        public virtual void Clear()
        {
            this.nodes = new LinkedList<TreeNode<T>>();
        }

        private LinkedList<TreeNode<T>> nodes;
        private T value;
    }
}
