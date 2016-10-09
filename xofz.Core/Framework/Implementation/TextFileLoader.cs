namespace xofz.Framework.Implementation
{
    using System.IO;
    using System.Text;

    public sealed class TextFileLoader : Loader
    {
        public TextFileLoader(Encoding encoding, bool returnArray)
        {
            this.encoding = encoding;
            this.returnArray = returnArray;
        }

        T Loader.Load<T>(string location)
        {
            if (typeof(T) != typeof(string))
            {
                return default(T);
            }

            if (returnArray)
            {
                return (T)(object)File.ReadAllLines(location, this.encoding);
            }

            return (T)(object)File.ReadAllText(location, this.encoding);
        }

        private readonly Encoding encoding;
        private readonly bool returnArray;
    }
}
