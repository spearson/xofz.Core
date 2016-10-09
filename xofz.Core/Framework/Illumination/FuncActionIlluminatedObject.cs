namespace xofz.Framework.Illumination
{
    using System;

    public sealed class FuncActionIlluminatedObject<T> : IlluminatedObject
    {
        public FuncActionIlluminatedObject(Func<T> func, Action<T> action) : base(new object[] { func, action })
        {
            this.func = func;
            this.action = action;
        }

        public T Create()
        {
            var t = this.func();
            this.action(t);

            return t;
        }

        private readonly Func<T> func;
        private readonly Action<T> action;
    }
}
