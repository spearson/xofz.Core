namespace xofz.Presentation
{
    using System;
    using System.Globalization;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public sealed class LogStatisticsPresenter : PopupPresenter
    {
        public LogStatisticsPresenter(
            LogStatisticsUi ui, 
            MethodWeb web) 
            : base(ui)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup(
            bool resetOnStart = false)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.resetOnStart = resetOnStart;
            this.ui.RangeKeyTapped += this.ui_RangeKeyTapped;
            this.ui.OverallKeyTapped += this.ui_OverallKeyTapped;
            this.ui.HideKeyTapped += this.Stop;
            this.resetDates();

            var w = this.web;
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) == 0)
            {
                return;
            }

            if (this.resetOnStart)
            {
                this.resetDates();
            }

            base.Start();
        }

        private void resetDates()
        {
            var w = this.web;
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<LogStatistics>(
                stats =>
                {
                    stats.Reset();
                    this.showStatistics(
                        stats, true);
                });
        }

        private void ui_RangeKeyTapped()
        {
            var w = this.web;
            var start = UiHelpers.Read(
                this.ui,
                () => this.ui.StartDate);
            var end = UiHelpers.Read(
                this.ui,
                () => this.ui.EndDate);
            w.Run<LogStatistics>(
                stats =>
                {
                    stats.ComputeRange(
                        start, end);
                    var typeInfo =
                        "Range: "
                        + this.formatDate(start)
                        + " to "
                        + this.formatDate(end);
                    UiHelpers.Write(
                        this.ui,
                        () => this.ui.TypeInfo = typeInfo);
                    this.ui.WriteFinished.WaitOne();
                    this.showStatistics(stats, false);
                });
        }

        private void ui_OverallKeyTapped()
        {
            var w = this.web;
            w.Run<LogStatistics>(
                stats =>
                {
                    stats.ComputeOverall();
                    UiHelpers.Write(
                        this.ui,
                        () => this.ui.TypeInfo = "Overall");
                    this.ui.WriteFinished.WaitOne();
                    this.showStatistics(stats, false);
                });
        }

        private void showStatistics(
            LogStatistics statistics,
            bool reset)
        {
            var defaultInfo = reset
                ? string.Empty
                : "No entries in range";

            var total = reset
                ? string.Empty
                : statistics.TotalEntryCount.ToString();
            var oldest = statistics.OldestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.OldestTimestamp);
            var newest = statistics.NewestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.NewestTimestamp);
            var earliest = statistics.EarliestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.EarliestTimestamp);
            var latest = statistics.LatestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.LatestTimestamp);
            var avgPerDay = reset
                ? string.Empty
                : Math.Round(statistics.AvgEntriesPerDay, 4)
                    .ToString(CultureInfo.CurrentUICulture);
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.TotalEntryCount = total;
                    this.ui.OldestTimestamp = oldest;
                    this.ui.NewestTimestamp = newest;
                    this.ui.EarliestTimestamp = earliest;
                    this.ui.LatestTimestamp = latest;
                    this.ui.AvgEntriesPerDay = avgPerDay;
                });
            this.ui.WriteFinished.WaitOne();
        }

        private string formatDate(DateTime date)
        {
            return date.ToString(
                @"yyyy/MM/dd");
        }

        private string formatTimestamp(DateTime timestamp)
        {
            return timestamp.ToString(
                @"yyyy/MM/dd HH:mm:ss");
        }

        private long setupIf1;
        private bool resetOnStart;
        private readonly LogStatisticsUi ui;
        private readonly MethodWeb web;
    }
}
