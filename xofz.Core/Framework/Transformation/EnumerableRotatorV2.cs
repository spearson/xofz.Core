namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lotters;

    public class EnumerableRotatorV2 : EnumerableRotator
    {
        public EnumerableRotatorV2()
            : this(new LinkedListLotter())
        {
        }

        public EnumerableRotatorV2(Lotter lotter)
        {
            if (lotter == null)
            {
                throw new ArgumentNullException(nameof(lotter));
            }

            this.lotter = lotter;
        }

        public virtual Lot<T> RotateV2<T>(
            IEnumerable<T> source,
            int cycles,
            bool goRight = true)
        {
            var rotated = base.Rotate(
                source,
                cycles,
                goRight);
            return this.lotter.Materialize(
                rotated);
        }

        protected readonly Lotter lotter;
    }
}
