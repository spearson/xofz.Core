namespace xofz.Misc.Framework.Illumination
{
    using System;
    using System.Collections.Generic;

    public sealed class AnimatedIlluminatedObject<T> 
        : IlluminatedObject
    {
        public AnimatedIlluminatedObject(
            IEnumerable<Action<T>> actions) 
            : base(new object[] { actions })
        {
            this.actions = actions;
        }

        public void Animate(
            T item)
        {
            var a = this.actions;
            if (a == null)
            {
                return;
            }

            foreach (var action in a)
            {
                action(item);
            }
        }

        private readonly IEnumerable<Action<T>> actions;
    }
}
