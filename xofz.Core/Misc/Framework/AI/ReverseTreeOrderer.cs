namespace xofz.Misc.Framework.AI
{
    using System.Linq;
    using xofz.Framework.Lots;

    public class ReverseTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual Lot<T> OrderedTree => this.currentTree;

        public virtual void Order(Tree<T> tree)
        {
            this.currentTree = new LinkedListLot<T>(
                tree.Reverse());
        }

        protected Lot<T> currentTree;
    }
}
