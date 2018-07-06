namespace xofz.Framework
{
    using System.Diagnostics;
    using System.Reflection;

    public class VersionReader
    {
        public virtual string Read()
        {
            var vi = FileVersionInfo.GetVersionInfo(
                Assembly.GetExecutingAssembly().Location);

            return vi.FileVersion;
        }
    }
}
