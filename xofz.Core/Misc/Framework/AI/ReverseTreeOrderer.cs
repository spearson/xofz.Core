namespace xofz.Misc.Framework.AI
{
    using System.Linq;
    using xofz.Framework.Lots;

    public class ReverseTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual Lot<T> OrderedTree => this.orderedTree;

        public virtual void Order(Tree<T> tree)
        {
            this.orderedTree = new LinkedListLot<T>(tree.Reverse());
        }

        private Lot<T> orderedTree;
    }
}
