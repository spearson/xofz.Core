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

        public void Setup(AccessLevel editLevel, bool resetOnStart = false)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.ui.StartDateChanged += this.ui_DateChanged;
            this.ui.EndDateChanged += this.ui_DateChanged;
            this.ui.AddKeyTapped += this.ui_AddKeyTapped;
            this.resetDates();
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.AddKeyVisible = false;
            });
            
            this.web.Subscribe<Log, LogEntry>(
                "EntryWritten", 
                this.log_EntryWritten);
            new Thread(this.timer_Elapsed).Start();

            this.web.Subscribe<xofz.Framework.Timer>(
                "Elapsed",
                this.timer_Elapsed,
                "LogTimer");
            this.web.Run<xofz.Framework.Timer>(
                t => t.Start(1000),
                "LogTimer");

            this.web.Run<Navigator>(n => n.RegisterPresenter(this));
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
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(7));
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
            });
        }

        private void ui_DateChanged()
        {
            var startDate = UiHelpers.Read(this.ui, () => this.ui.StartDate);
            var endDate = UiHelpers.Read(this.ui, () => this.ui.EndDate);

            var entries = this.web.Run<Log, List<LogEntry>>(
                l => l.ReadEntries()
                    .Where(e =>
                        e.Timestamp >= startDate
                        && e.Timestamp < endDate.AddDays(1))
                    .OrderByDescending(e => e.Timestamp)
                    .ToList());

            var uiEntries = new LinkedListMaterializedEnumerable<
                Tuple<string, string, string>>(
                entries.Select(this.createTuple));
            
            UiHelpers.Write(this.ui, () => this.ui.Entries = uiEntries);
        }

        private void log_EntryWritten(LogEntry e)
        {
            if (UiHelpers.Read(this.ui, () => this.ui.EndDate) < DateTime.Today)
            {
                return;
            }

            lock (this.locker)
            {
                var newEntries = new LinkedList<
                    Tuple<string, string, string>>(
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

        private void ui_AddKeyTapped()
        {
            this.web.Run<Navigator>(
                n => n.PresentFluidly<LogEditorPresenter>());
        }

        private void timer_Elapsed()
        {
            var cal = this.web.Run<AccessController, AccessLevel>(
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
