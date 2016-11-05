namespace xofz.UI
{
    using System;

    public interface LogUi : Ui
    {
        event Action StartDateChanged;

        event Action EndDateChanged;

        event Action AddKeyTapped;

        MaterializedEnumerable<Tuple<string, string, string>> Entries { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        bool AddKeyVisible { get; set; }
    }
}
