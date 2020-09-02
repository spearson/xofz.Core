namespace xofz.Misc.Framework.Illumination
{
    using System.Reflection;
    using static EnumerableHelpers;

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
            var field = FirstOrDefault(
                fields,
                fi => fi.Name == fieldName);
            return (T)field?.GetValue(this.dependency);
        }

        private readonly object dependency;
    }
}
