namespace xofz.Misc.Framework.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using xofz.Misc.Framework.Theory;

    public static class FileHelpers
    {
        public static IEnumerable<bool> ReadAllBits(string filePath)
        {
            return new BinaryTranslator().GetBits(File.ReadAllBytes(filePath));
        }

        public static void WriteAllBits(string filePath, IEnumerable<bool> bits)
        {
            var bt = new BinaryTranslator();
            File.WriteAllBytes(filePath, bt.GetBytes(bits).ToArray());
        }
    }
}
