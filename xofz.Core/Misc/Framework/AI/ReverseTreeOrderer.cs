namespace xofz.Misc.Framework.AI
{
    using xofz.Framework.Lots;
    using static EnumerableHelpers;

    public class ReverseTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual Lot<T> OrderedTree => this.currentTree;

        public virtual void Order(Tree<T> tree)
        {
            this.currentTree = new LinkedListLot<T>(
                Reverse(
                    tree));
        }

        protected Lot<T> currentTree;
    }
}
