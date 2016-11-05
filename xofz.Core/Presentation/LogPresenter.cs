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
            Log log)
            : base(ui, shell)
        {
            this.ui = ui;
            this.log = log;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.StartDateChanged += this.ui_DateChanged;
            this.ui.EndDateChanged += this.ui_DateChanged;
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.StartDate = DateTime.Today.Subtract(TimeSpan.FromDays(7));
                this.ui.EndDate = DateTime.Today;
            });
            
            new Thread(this.ui_DateChanged).Start();
            this.log.EntryWritten += this.log_EntryWritten;
        }

        private void ui_DateChanged()
        {
            var startDate = UiHelpers.Read(this.ui, () => this.ui.StartDate);
            var endDate = UiHelpers.Read(this.ui, () => this.ui.EndDate);

            var entries = this.log.ReadEntries().ToList();
            entries = entries
                .Where(e =>
                    e.Timestamp >= startDate
                    && e.Timestamp <= endDate.AddDays(1)).ToList();
            var uiEntries
                = new LinkedListMaterializedEnumerable<Tuple<string, string, string>>(
                entries.Select(this.createTuple));
            
            UiHelpers.Write(this.ui, () => this.ui.Entries = uiEntries);
        }

        private void log_EntryWritten(LogEntry e)
        {
            if (!UiHelpers.Read(this.ui, () => this.ui.EndDate < DateTime.Today))
            {
                return;
            }

            var newEntries = new LinkedList<
                Tuple<string, string, string>>(
                UiHelpers.Read(this.ui, () => this.ui.Entries));
            newEntries.AddFirst(this.createTuple(e));

            UiHelpers.Write(this.ui, () => this.ui.Entries =
                new LinkedListMaterializedEnumerable<
                    Tuple<string, string, string>>(newEntries));
        }

        private Tuple<string, string, string> createTuple(LogEntry e)
        {
            return Tuple.Create(
                e.Timestamp.ToString("yyyy/MM/dd hh:mm.ss tt", CultureInfo.CurrentCulture),
                e.Type,
                string.Join(Environment.NewLine, e.Content));
        }

        private int setupIf1;
        private readonly LogUi ui;
        private readonly Log log;
    }
}
