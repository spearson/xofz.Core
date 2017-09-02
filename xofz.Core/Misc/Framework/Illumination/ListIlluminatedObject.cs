namespace xofz.Misc.Framework.Illumination
{
    using System.Collections.Generic;

    public sealed class ListIlluminatedObject<T> : IlluminatedObject
    {
        public ListIlluminatedObject(IList<T> list) : base(new object[] { list })
        {
            this.list = list;
        }

        public T this[int index]
        {
            get => this.list[index];

            set => this.list[index] = value;
        }

        private readonly IList<T> list;
    }
}
