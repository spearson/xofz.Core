namespace xofz.Misc.Framework.Erudition
{
    using xofz.Framework.Transformation;
    using static EnumerableHelpers;

    public class LiberatedObject
    {
        public LiberatedObject(
            params object[] dependencies)
            : this(new EnumerableRotator(), 
                dependencies)
        {
        }

        public LiberatedObject(
            EnumerableRotator rotator, 
            params object[] dependencies)
        {
            this.rotator = rotator ?? new EnumerableRotator();
            this.dependencies = dependencies ?? new object[0];
        }

        public virtual int DependencyCount => this.dependencies.Length;

        public virtual dynamic this[int index] => this.dependencies[index];

        public virtual T GetDependency<T>()
        {
            return FirstOrDefault(
                OfType<T>(
                    this.dependencies));
        }

        public virtual void ShiftDependencies(
            bool shiftRight)
        {
            this.setDependencies(
                ToArray(
                    this.rotator.Rotate(
                        this.dependencies,
                        1,
                        shiftRight)));
        }

        protected virtual void setDependencies(
            object[] dependencies)
        {
            this.dependencies = dependencies;
        }

        protected object[] dependencies;
        protected readonly EnumerableRotator rotator;
    }
}
