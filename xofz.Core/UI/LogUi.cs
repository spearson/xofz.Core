namespace xofz.UI
{
    using System;
    using System.Collections.Generic;

    public interface LogUi : Ui
    {
        MaterializedEnumerable<Tuple<string, string, string>> Entries { get; set; }

        string Location { get; set; }
    }
}
