namespace xofz.Apps.Connector.Framework
{
    using System.Collections.Generic;

    public interface Reader
    {
        IEnumerable<object>[] Read(string location);
    }
}
