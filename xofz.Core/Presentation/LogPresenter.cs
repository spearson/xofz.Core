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
            Log log) : base(ui, shell)
        {
            this.ui = ui;
            this.log = log;
        }

        public void Setup(string location)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Location = location;
                this.ui.Entries = new LinkedListMaterializedEnumerable<Tuple<string, string, string>>(
                    new LinkedList<Tuple<string, string, string>>(this.log
                        .ReadEntries()
                        .Select(this.createTuple)));
            });
            
            this.log.EntryWritten += this.log_EntryWritten;
        }

        private void log_EntryWritten(LogEntry e)
        {
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
                e.Timestamp.ToString("yyyy/MM/dd hh:mm.ss", CultureInfo.CurrentCulture),
                e.Type,
                string.Join(Environment.NewLine, e.Content));
        }

        private int setupIf1;
        private readonly LogUi ui;
        private readonly Log log;
    }
}
