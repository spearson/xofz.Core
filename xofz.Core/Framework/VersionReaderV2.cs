namespace xofz.Framework
{
    using System.Reflection;

    public class VersionReaderV2 : VersionReader
    {
        public VersionReaderV2(
            Assembly executingAssembly)
            : base(executingAssembly)
        {
        }
        
        public virtual string ReadCoreVersionV2()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.readProtected(ea);
        }
    }
}
