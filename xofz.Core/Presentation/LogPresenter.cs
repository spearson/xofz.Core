namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Framework;
    using Framework.Materialization;
    using UI;

    public sealed class LogPresenter : Presenter
    {
        public LogPresenter(
            LogUi ui, 
            ShellUi shell,
            MethodWeb web)
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
            this.locker = new object();
        }

        public void Setup(
            AccessLevel editLevel, 
            bool resetOnStart = false,
            bool statisticsEnabled = false)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.ui.StartDateChanged += this.ui_DateChanged;
            this.ui.EndDateChanged += this.ui_DateChanged;
            this.ui.AddKeyTapped += this.ui_AddKeyTapped;
            this.ui.StatisticsKeyTapped += this.ui_StatisticsKeyTapped;
            this.ui.FilterTextChanged += this.ui_FilterTextChanged;
            this.resetDatesAndFilters();
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.AddKeyVisible = false;
                this.ui.StatisticsKeyVisible = statisticsEnabled;
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<Log>(l => l.EntryWritten += this.log_EntryWritten);
            new Thread(this.timer_Elapsed).Start();

            w.Run<xofz.Framework.Timer>(
                t =>
                {
                    t.Elapsed += this.timer_Elapsed;
                    t.Start(1000);
                },
                "LogTimer");
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
                this.resetDatesAndFilters();
            }

            base.Start();
        }

        private void resetDatesAndFilters()
        {
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
                this.ui.FilterContent = string.Empty;
                this.ui.FilterType = string.Empty;
            });
            this.ui.WriteFinished.WaitOne();
        }

        private void ui_DateChanged()
        {
            this.reloadEntries();
        }

        private void reloadEntries()
        {
            var w = this.web;
            var start = UiHelpers.Read(this.ui, () => this.ui.StartDate);
            var end = UiHelpers.Read(this.ui, () => this.ui.EndDate);
            var filterContent = UiHelpers.Read(
                this.ui, 
                () => this.ui.FilterContent);
            var filterType = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterType);
            w.Run<Log>(l =>
            {
                // first, begin reading all entries
                var matchingEntries = l.ReadEntries();

                // second, get all the entries in the date range
                matchingEntries = matchingEntries.Where(
                    e => e.Timestamp >= start
                         && e.Timestamp < end.AddDays(1));

                // third, match on content
                if (!string.IsNullOrWhiteSpace(filterContent))
                {
                    matchingEntries = matchingEntries.Where(
                        e => e.Content.Any(s => s.ToLowerInvariant()
                            .Contains(filterContent.ToLowerInvariant())));
                }

                // fourth, match on type
                if (!string.IsNullOrWhiteSpace(filterType))
                {
                    matchingEntries = matchingEntries.Where(
                        e => e.Type.ToLowerInvariant()
                            .Contains(filterType.ToLowerInvariant()));
                }

                // finally, order them by newest first
                matchingEntries = matchingEntries.OrderByDescending(
                    e => e.Timestamp);

                var uiEntries = new LinkedListMaterializedEnumerable<
                    Tuple<string, string, string>>(
                    matchingEntries.Select(this.createTuple));

                UiHelpers.Write(
                    this.ui, 
                    () => this.ui.Entries = uiEntries);
                this.ui.WriteFinished.WaitOne();
            });
        }

        private void ui_AddKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogEditorPresenter>());
        }

        private void ui_StatisticsKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogStatisticsPresenter>());
        }

        private void ui_FilterTextChanged()
        {
            this.reloadEntries();
        }

        private void log_EntryWritten(LogEntry e)
        {
            if (UiHelpers.Read(this.ui, () => this.ui.EndDate) < DateTime.Today)
            {
                return;
            }

            lock (this.locker)
            {
                var newEntries = new LinkedList<Tuple<string, string, string>>(
                    UiHelpers.Read(this.ui, () => this.ui.Entries));
                newEntries.AddFirst(this.createTuple(e));

                UiHelpers.Write(this.ui, () => this.ui.Entries =
                    new LinkedListMaterializedEnumerable<
                        Tuple<string, string, string>>(newEntries));
                this.ui.WriteFinished.WaitOne();
            }
        }

        private Tuple<string, string, string> createTuple(LogEntry e)
        {
            return Tuple.Create(
                e.Timestamp.ToString("yyyy/MM/dd HH:mm.ss", CultureInfo.CurrentCulture),
                e.Type,
                string.Join(Environment.NewLine, e.Content));
        }

        private void timer_Elapsed()
        {
            var w = this.web;
            var cal = w.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);
            var visible = cal >= this.editLevel;
            UiHelpers.Write(this.ui, () => this.ui.AddKeyVisible = visible);
        }

        private long setupIf1;
        private bool resetOnStart;
        private AccessLevel editLevel;
        private readonly LogUi ui;
        private readonly MethodWeb web;
        private readonly object locker;
    }
}
