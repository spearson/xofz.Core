namespace xofz.Misc.Framework.Illumination
{
    using System;

    public sealed class FuncIlluminatedObject<T> 
        : IlluminatedObject
    {
        public FuncIlluminatedObject(
            Func<T> generator) 
            : base(new object[] { generator })
        {
            this.generator = generator;
        }

        public T Generate() => this.generator();

        private readonly Func<T> generator;
    }
}
