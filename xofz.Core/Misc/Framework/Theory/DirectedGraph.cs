namespace xofz.Misc.Framework.Theory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Misc.Framework.AI;

    public class DirectedGraph
    {
        public virtual IEnumerable<T> TakePath<T>(
            Tree<T> tree, 
            Func<T, int> pathChooser)
        {
            if (tree == null)
            {
                yield break;
            }

            var tn = tree.Node;
            yield return tn.Value;

            var nextNode = tn
                .Nodes
                ?.Skip(pathChooser(tn.Value))
                .FirstOrDefault();
            if (nextNode == null)
            {
                yield break;
            }

            foreach (var item in this.TakePath(
                new Tree<T>(nextNode), 
                pathChooser))
            {
                yield return item;
            }
        }
    }
}
