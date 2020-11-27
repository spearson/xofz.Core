namespace xofz.Framework
{
    using System;
    using System.Reflection;

    public class VersionReaderV2 
        : VersionReader
    {
        public VersionReaderV2()
        {
        }

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

        public virtual Version ReadCoreVersionAsVersionV2()
        {
            var ea = Assembly.GetExecutingAssembly();
            return this.ReadAsVersion(ea);
        }
    }
}
