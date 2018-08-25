namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class EnumerableRotatorV2 : EnumerableRotator
    {
        public EnumerableRotatorV2()
            : this(new LinkedListMaterializer())
        {
        }

        public EnumerableRotatorV2(Materializer materializer)
        {
            if (materializer == null)
            {
                throw new ArgumentNullException(nameof(materializer));
            }

            this.materializer = materializer;
        }

        public virtual MaterializedEnumerable<T> RotateV2<T>(
            IEnumerable<T> source,
            int cycles,
            bool goRight = true)
        {
            var rotated = base.Rotate(
                source,
                cycles,
                goRight);
            return this.materializer.Materialize(
                rotated);
        }

        protected readonly Materializer materializer;
    }
}
