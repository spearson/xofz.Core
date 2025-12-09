namespace xofz.UI.Forms
{
    using System;
    using System.Windows.Forms;
    using xofz.UI.Forms.Internal;

    // lots of code jacked from http://www.codeproject.com/Articles/18440/DataGridView-Multi-column-Sort
    public class MultiColumnSortDataGridView 
        : DataGridView
    {
        public MultiColumnSortDataGridView()
        {
            this.columnSorter = new ColumnSorter(this);
            this.MaxSortColumns = 0;
        }

        protected override void OnColumnHeaderMouseClick(
            DataGridViewCellMouseEventArgs e)
        {
            this.columnSorter.SetSortColumn(
                e.ColumnIndex, 
                ModifierKeys);

            this.Sort(this.columnSorter);

            this.Columns[e.ColumnIndex].SortMode 
                = DataGridViewColumnSortMode.Programmatic;

            base.OnColumnHeaderMouseClick(e);
        }

        public int MaxSortColumns
        {
            get => this.columnSorter.MaxSortColumns;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(this.MaxSortColumns), 
                        @"MaxSortColumns must be >= 0; set to 0 for no limit");
                }

                this.columnSorter.MaxSortColumns = value;
            }
        }

        public virtual string SortOrderDescription => 
            this.columnSorter.SortOrderDescription;

        public virtual void Sort()
        {
            this.Sort(
                this.columnSorter);
        }

        private readonly ColumnSorter columnSorter;
    }
}
