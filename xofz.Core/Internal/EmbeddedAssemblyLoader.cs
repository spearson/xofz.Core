namespace xofz.Internal
{
    using System;
    using System.Reflection;

    internal static class EmbeddedAssemblyLoader
    {
        public static readonly object GlobalLock = new object();

        public static Assembly Load(object sender, ResolveEventArgs e)
        {
            if (e.Name.Contains("VncSharp"))
            {
                return Assembly.Load(Resources.VncSharp);
            }

            if (e.Name.Contains("Unme"))
            {
                return Assembly.Load(Resources.Unme_Common);
            }

            if (e.Name.Contains("log4net"))
            {
                return Assembly.Load(Resources.log4net);
            }

            if (e.Name.Contains("Modbus"))
            {
                return Assembly.Load(Resources.Modbus);
            }

            return null;
        }
    }
}
