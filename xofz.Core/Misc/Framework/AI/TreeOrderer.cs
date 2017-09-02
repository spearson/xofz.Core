namespace xofz.Misc.Framework.AI
{
    public interface TreeOrderer<T>
    {
        MaterializedEnumerable<T> OrderedTree { get; }

        void Order(Tree<T> tree);
    }
}