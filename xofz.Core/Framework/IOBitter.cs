namespace xofz.Framework
{
    using System.Collections.Generic;

    public interface IOBitter
    {
        IEnumerable<bool> Read(string location);

        void Write(
            IEnumerable<bool> bits, 
            string location,
            out bool succeeded);
    }
}
