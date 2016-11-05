namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.Framework.Materialization;

    public partial class UserControlLogUi : UserControlUi, LogUi
    {
        public UserControlLogUi()
        {
            this.InitializeComponent();

            var h = this.Handle;
        }

        public event Action StartDateChanged;

        public event Action EndDateChanged;

        MaterializedEnumerable<Tuple<string, string, string>> LogUi.Entries
        {
            get
            {
                var ll = new LinkedList<Tuple<string, string, string>>();
                foreach (DataGridViewRow row in this.entriesGrid.Rows)
                {
                    ll.AddLast(
                        Tuple.Create(
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString()));
                }

                return new LinkedListMaterializedEnumerable<
                    Tuple<string, string, string>>(ll);
            }

            set
            {
                var eg = this.entriesGrid;
                eg.Rows.Clear();
                foreach (var entry in value)
                {
                    eg.Rows.Add(entry.Item1, entry.Item2, entry.Item3);
                }
            }
        }

        DateTime LogUi.StartDate
        {
            get { return this.startDatePicker.SelectionStart; }

            set
            {
                this.startDatePicker.SelectionStart = value;
                this.startDatePicker.SelectionEnd = value;
            }
        }

        DateTime LogUi.EndDate
        {
            get { return this.endDatePicker.SelectionStart; }

            set
            {
                this.endDatePicker.SelectionStart = value;
                this.endDatePicker.SelectionEnd = value;
            }
        }

        private void startDatePicker_DateSelected(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.StartDateChanged?.Invoke()).Start();
        }

        private void endDatePicker_DateSelected(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.EndDateChanged?.Invoke()).Start();
        }
    }
}
