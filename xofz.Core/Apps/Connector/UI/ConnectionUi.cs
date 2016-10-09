namespace xofz.Apps.Connector.UI
{
    using System.Collections.Generic;
    using xofz.UI;

    public interface ConnectionUi : Ui
    {
        IEnumerable<object> Connection { get; set; }
    }
}
