namespace xofz.Misc.Framework.Illumination
{
    using System;
    using System.Collections.Generic;
    using EH = xofz.EnumerableHelpers;

    public sealed class GatedIlluminatedObject<T> 
        : IlluminatedObject
    {
        public GatedIlluminatedObject(
            IEnumerable<bool> gates, 
            Func<bool, T> generator)
            : base(new object[] { gates, generator })
        {
            this.gates = gates;
            this.generator = generator;
        }

        public IEnumerable<T> Generate()
        {
            return EH.Select(
                this.gates,
                gate => this.generator(gate))
                ?? EH.Empty<T>();
        }

        private readonly IEnumerable<bool> gates;
        private readonly Func<bool, T> generator;
    }
}
