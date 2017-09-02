namespace xofz.Misc.Framework.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Materialization;

    public class Tree<T> : MaterializedEnumerable<T>
    {
        public Tree() : this(new TreeNode<T>())
        {
        }

        public Tree(TreeNode<T> node)
        {
            this.node = node;
        }

        public virtual TreeNode<T> Node => this.node;

        public virtual long Count => this.enumerate(this.node).Count;

        public virtual int ComputeDepth()
        {
            return this.deepen(this.node, 1);
        }

        private int deepen(TreeNode<T> node, int currentDepth)
        {
            if (node.Nodes.Count == 0)
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
            return this.enumerate(this.node).GetEnumerator();
        }

        private List<T> enumerate(TreeNode<T> node)
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

        private readonly TreeNode<T> node;
    }

    public class TreeNode<T>
    {
        public TreeNode()
            : this(default(T))
        {
        }

        public TreeNode(T value)
        {
            this.value = value;
            this.nodes = new LinkedList<TreeNode<T>>();
        }

        public virtual T Value
        {
            get => this.value;

            set => this.value = value;
        }

        public virtual MaterializedEnumerable<TreeNode<T>> Nodes
            => new LinkedListMaterializedEnumerable<TreeNode<T>>(this.nodes);

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
