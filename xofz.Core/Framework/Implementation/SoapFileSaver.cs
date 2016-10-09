namespace xofz.Framework.Implementation
{
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Soap;

    public sealed class SoapFileSaver : Saver
    {
        public void Save<T>(string location, T value)
        {
            var stream = new MemoryStream { Capacity = Marshal.SizeOf<T>() };
            new SoapFormatter().Serialize(stream, value);
            File.WriteAllBytes(location, stream.ToArray());
        }
    }
}
