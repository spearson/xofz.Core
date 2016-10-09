using System.Collections.Generic;

namespace xofz.Apps.ImageSplicer.Framework
{
    public interface ImageReader
    {
        IEnumerable<object>[] Read(string location);
    }
}
