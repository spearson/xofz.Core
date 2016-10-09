namespace xofz.Framework.Illumination
{
    using System;
    using System.Collections.Generic;

    public sealed class AnimatedIlluminatedObject<T> : IlluminatedObject
    {
        public AnimatedIlluminatedObject(IEnumerable<Action<T>> actions) : base(new object[] { actions })
        {
            this.actions = actions;
        }

        public void Animate(T item)
        {
            foreach (var action in this.actions)
            {
                action(item);
            }
        }

        private readonly IEnumerable<Action<T>> actions;
    }
}
