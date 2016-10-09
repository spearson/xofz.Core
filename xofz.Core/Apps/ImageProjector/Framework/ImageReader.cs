namespace xofz.Apps.ImageProjector.Framework
{
    using System.Collections.Generic;

    public interface ImageReader
    {
        IEnumerable<object> Read(string location);
    }
}
