namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class EnumerableTrapper<T>
    {
        public virtual MaterializedEnumerable<T> TrappedCollection => 
            new LinkedListMaterializedEnumerable<T>(this.trapper);

        public virtual IEnumerable<T> Trap(IEnumerable<T> source)
        {
            this.setTrapper(new LinkedList<T>());
            var t = this.trapper;
            foreach (var item in source)
            {
                t.AddLast(item);
                yield return item;
            }
        }

        private void setTrapper(LinkedList<T> trapper)
        {
            this.trapper = trapper;
        }

        private LinkedList<T> trapper;
    }
}
