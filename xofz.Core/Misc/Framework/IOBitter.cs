namespace xofz.Misc.Framework
{
    using System.Collections.Generic;

    public interface IOBitter
        : Nameable
    {
        IEnumerable<bool> Read();

        void Write(
            IEnumerable<bool> bits, 
            out bool succeeded);
    }
}
