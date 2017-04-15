namespace xofz.Framework
{
    using Transformation;

    public class All<T>
    {
        public All(
            EnumerableSplicer splicer,
            Beholder<T> beholder,
            Nightfall nightfall)
        {
            this.Splicer = splicer;
            this.Beholder = beholder;
            this.Nightfall = nightfall;
        }

        public virtual EnumerableSplicer Splicer { get; }

        public virtual Beholder<T> Beholder { get; }

        public virtual Nightfall Nightfall { get; }
    }
}
