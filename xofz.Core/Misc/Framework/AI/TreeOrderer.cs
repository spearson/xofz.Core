namespace xofz.Misc.Framework.AI
{
    public interface TreeOrderer<T>
    {
        Lot<T> OrderedTree { get; }

        void Order(Tree<T> tree);
    }
}