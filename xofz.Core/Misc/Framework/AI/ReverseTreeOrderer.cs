namespace xofz.Misc.Framework.AI
{
    using System.Linq;
    using xofz.Framework.Materialization;

    public class ReverseTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual MaterializedEnumerable<T> OrderedTree => this.orderedTree;

        public virtual void Order(Tree<T> tree)
        {
            this.orderedTree = new LinkedListMaterializedEnumerable<T>(tree.Reverse());
        }

        private MaterializedEnumerable<T> orderedTree;
    }
}
