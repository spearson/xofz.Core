namespace xofz.Misc.Framework.Computation
{
    using System;
    using System.Collections.Generic;

    public class Emanator<T>
    {
        public Emanator()
            : this(new List<T>())
        {
        }

        public Emanator(IList<T> list)
        {
            this.list = list;
        }

        public virtual T Left => this.list[0];

        public virtual T Right
        {
            get
            {
                var l = this.list;
                return l[l.Count - 1];
            }
        }

        public virtual void AddItem(T item, bool emanateRight)
        {
            var l = this.list;
            var index = (l.Count / 2) + (emanateRight ? 1 : 0);
            l.Insert(index, item);
        }

        public virtual Tuple<T, T> Emanate(bool rightFirst)
        {
            var l = this.list;
            if (l.Count == 1)
            {
                return Tuple.Create(l[0], l[0]);
            }

            var tuple = rightFirst
                ? Tuple.Create(this.Right, this.Left)
                : Tuple.Create(this.Left, this.Right);

            l.Remove(this.Right);
            l.Remove(this.Left);

            return tuple;
        }

        private readonly IList<T> list;
    }
}
