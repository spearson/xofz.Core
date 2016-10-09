namespace xofz.Framework.Illumination
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class MaterializedEnumerableIlluminatedObject<T> : IlluminatedObject, MaterializedEnumerable<T>
    {
        public MaterializedEnumerableIlluminatedObject(MaterializedEnumerable<T> collection)
            : base(new object[] { collection })
        {
            this.collection = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public long Count => this.collection.Count;

        private readonly MaterializedEnumerable<T> collection;
    }
}
