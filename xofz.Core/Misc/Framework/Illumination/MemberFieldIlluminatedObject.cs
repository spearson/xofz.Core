namespace xofz.Misc.Framework.Illumination
{
    using System.Linq;
    using System.Reflection;

    public sealed class MemberFieldIlluminatedObject 
        : IlluminatedObject
    {
        public MemberFieldIlluminatedObject(
            object dependency)
            : base(new[] { dependency })
        {
            this.dependency = dependency;
        }

        public T Get<T>(string fieldName)
        {
            // a wee bit of reflection
            var fields = this.dependency.GetType().GetFields(
                BindingFlags.NonPublic | BindingFlags.Instance);
            var field = fields.FirstOrDefault(
                fi => fi.Name == fieldName);
            return (T)field?.GetValue(this.dependency);
        }

        private readonly object dependency;
    }
}
