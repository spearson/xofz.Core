namespace xofz.Misc.Framework.Illumination
{
    using System;

    public sealed class FuncActionIlluminatedObject<T> 
        : IlluminatedObject
    {
        public FuncActionIlluminatedObject(
            Func<T> func, 
            Action<T> action) 
            : base(new object[] { func, action })
        {
            this.func = func;
            this.action = action;
        }

        public T Create()
        {
            var f = this.func;
            if (f == null)
            {
                return default;
            }

            var t = f();
            this.action(t);

            return t;
        }

        private readonly Func<T> func;
        private readonly Action<T> action;
    }
}
