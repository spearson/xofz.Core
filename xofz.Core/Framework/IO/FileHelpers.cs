namespace xofz.Framework.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using xofz.Framework.Theory;

    public static class FileHelpers
    {
        public static IEnumerable<bool> ReadAllBits(string filePath)
        {
            var bytes = new LinkedList<byte>();
            using (var reader = File.OpenRead(filePath))
            {
                while (reader.Position < reader.Length)
                {
                    bytes.AddLast((byte)reader.ReadByte());
                }
            }

            return new BinaryTranslator().GetBits(bytes);
        }

        public static void WriteAllBits(string filePath, IEnumerable<bool> bits)
        {
            var bt = new BinaryTranslator();
            File.WriteAllBytes(filePath, bt.GetBytes(bits).ToArray());
        }
    }
}
