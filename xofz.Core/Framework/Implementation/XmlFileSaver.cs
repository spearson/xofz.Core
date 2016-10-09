namespace xofz.Framework.Implementation
{
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;

    public sealed class XmlFileSaver : Saver
    {
        public void Save<T>(string location, T value)
        {
            var stream = new MemoryStream { Capacity = Marshal.SizeOf<T>() };
            new XmlSerializer(typeof(T)).Serialize(stream, value);
            File.WriteAllBytes(location, stream.ToArray());
        }
    }
}
