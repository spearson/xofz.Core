namespace xofz.Internal
{
    using System;
    using System.Reflection;

    internal static class EmbeddedAssemblyLoader
    {
        public static Assembly Load(object sender, ResolveEventArgs e)
        {
            if (e.Name.EndsWith("Retargetable=Yes"))
            {
                return Assembly.Load(new AssemblyName(e.Name));
            }

            var container = Assembly.GetExecutingAssembly();
            var path = new AssemblyName(e.Name).Name + ".dll";

            using (var stream = container.GetManifestResourceStream(path))
            {
                if (stream == null)
                {
                    return null;
                }

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Assembly.Load(bytes);
            }
        }
    }
}
