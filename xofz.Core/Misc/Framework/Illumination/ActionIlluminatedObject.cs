namespace xofz.Misc.Framework.Illumination
{
    using System;

    public sealed class ActionIlluminatedObject<T> : IlluminatedObject
    {
        public ActionIlluminatedObject(Action<T> action) 
            : base(new object[] { action })
        {
            this.action = action;
        }

        public void Act(
            T item)
        {
            this.action(item);
        }

        private readonly Action<T> action;
    }
}
