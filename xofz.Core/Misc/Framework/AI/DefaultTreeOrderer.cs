namespace xofz.Misc.Framework.AI
{
    public class DefaultTreeOrderer<T> 
        : TreeOrderer<T>
    {
        public virtual Lot<T> OrderedTree 
            => this.currentTree;

        public virtual void Order(
            Tree<T> tree)
        {
            this.currentTree = tree;
        }

        protected Lot<T> currentTree;
    }
}
