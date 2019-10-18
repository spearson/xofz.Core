namespace xofz.Misc.Framework.Implementation
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public sealed class BinaryFileLoader : Loader
    {
        public T Load<T>(
            string location)
        {
            var bytes = File.ReadAllBytes(location);
            return (T)new BinaryFormatter().Deserialize(new MemoryStream(bytes));
        }
    }
}
