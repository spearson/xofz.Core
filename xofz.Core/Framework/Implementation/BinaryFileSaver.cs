namespace xofz.Framework.Implementation
{
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    public sealed class BinaryFileSaver : Saver
    {
        public void Save<T>(string location, T value)
        {
            var stream = new MemoryStream { Capacity = Marshal.SizeOf<T>() };
            new BinaryFormatter().Serialize(stream, value);
            File.WriteAllBytes(location, stream.ToArray());
        }
    }
}
