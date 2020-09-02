namespace xofz.Misc.Framework.Illumination
{
    using EH = xofz.EnumerableHelpers;

    public class IlluminatedObject
    {
        public IlluminatedObject(
            object[] dependencies)
        {
            this.dependencies = dependencies 
                                ?? new object[0];
        }

        public virtual int DependencyCount
        {
            get
            {
                var sum = 0;
                foreach (var dependency in this.dependencies)
                {
                    if (dependency is IlluminatedObject)
                    {
                        checked
                        {
                            sum += ((IlluminatedObject)dependency).DependencyCount;
                        }

                        continue;
                    }

                    checked
                    {
                        ++sum;
                    }
                }

                return sum;
            }
        }

        public override int GetHashCode()
        {
            return EH.Aggregate(
                this.dependencies,
                0,
                (current, o) => current ^ o.GetHashCode());
        }

        public override bool Equals(
            object obj)
        {
            return EH.All(
                this.dependencies,
                t => t.Equals(obj));
        }

        protected readonly object[] dependencies;
    }
}
