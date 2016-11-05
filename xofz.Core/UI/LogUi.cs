namespace xofz.UI
{
    using System;

    public interface LogUi : Ui
    {
        event Action StartDateChanged;

        event Action EndDateChanged;

        MaterializedEnumerable<Tuple<string, string, string>> Entries { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }
    }
}
