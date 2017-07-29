namespace xofz.UI
{
    using System;

    public interface LogStatisticsUi : PopupUi
    {
        event Action OverallKeyTapped;

        event Action RangeKeyTapped;

        event Action HideKeyTapped;

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string TypeInfo { get; set; }

        string TotalEntryCount { get; set; }

        string AvgEntriesPerDay { get; set; }

        string OldestTimestamp { get; set; }

        string NewestTimestamp { get; set; }

        string EarliestTimestamp { get; set; }

        string LatestTimestamp { get; set; }
    }
}
