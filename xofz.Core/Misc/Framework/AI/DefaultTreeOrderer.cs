﻿namespace xofz.Misc.Framework.AI
{
    public class DefaultTreeOrderer<T> : TreeOrderer<T>
    {
        public virtual MaterializedEnumerable<T> OrderedTree => this.orderedTree;

        public virtual void Order(Tree<T> tree)
        {
            this.orderedTree = tree;
        }

        private MaterializedEnumerable<T> orderedTree;
    }
}