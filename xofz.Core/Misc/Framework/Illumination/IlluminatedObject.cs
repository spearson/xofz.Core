namespace xofz.Misc.Framework.Illumination
{
    using System.Linq;

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
            return this.dependencies
                .Aggregate(0, (current, o) => current ^ o.GetHashCode());
        }

        public override bool Equals(
            object obj)
        {
            return this.dependencies
                .All(t => t.Equals(obj));
        }

        protected readonly object[] dependencies;
    }
}
