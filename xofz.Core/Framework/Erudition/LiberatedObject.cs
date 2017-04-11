namespace xofz.Framework.Erudition
{
    using System.Collections.Generic;
    using System.Linq;
    using Transformation;

    public class LiberatedObject
    {
        public LiberatedObject(params object[] dependencies)
            : this(new EnumerableRotator(), dependencies)
        {
        }

        public LiberatedObject(EnumerableRotator rotator, params object[] dependencies)
        {
            this.rotator = rotator;
            this.dependencies = dependencies;
        }

        public virtual int DependencyCount => this.dependencies.Count;

        public virtual dynamic this[int index] => this.dependencies[index];

        public virtual T GetDependency<T>()
        {
            return this.dependencies.OfType<T>().FirstOrDefault();
        }

        public virtual void ShiftDependencies(bool shiftRight)
        {
            this.setDependencies(
                this.rotator.Rotate(
                    this.dependencies, 1, shiftRight).ToList());
        }

        private void setDependencies(IReadOnlyList<object> dependencies)
        {
            this.dependencies = dependencies;
        }

        private IReadOnlyList<object> dependencies;
        private readonly EnumerableRotator rotator;
    }
}
