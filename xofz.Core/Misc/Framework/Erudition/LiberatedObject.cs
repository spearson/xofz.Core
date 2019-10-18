namespace xofz.Misc.Framework.Erudition
{
    using System.Linq;
    using xofz.Framework.Transformation;

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
            return this.dependencies.OfType<T>().FirstOrDefault();
        }

        public virtual void ShiftDependencies(
            bool shiftRight)
        {
            this.setDependencies(
                this.rotator.Rotate(
                        this.dependencies,
                        1,
                        shiftRight)
                    .ToArray());
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
