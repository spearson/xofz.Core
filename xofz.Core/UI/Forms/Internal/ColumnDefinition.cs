namespace xofz.UI.Forms.Internal
{
    using System;
    using System.Windows.Forms;

    // taken from http://www.codeproject.com/Articles/18440/DataGridView-Multi-column-Sort
    internal struct ColumnDefinition
    {
        internal readonly short columnNumber;
        internal bool ascending;

        internal ColumnDefinition(
            int columnNumber, 
            SortOrder sortOrder)
        {
            this.columnNumber = Convert.ToInt16(columnNumber);
            this.ascending = sortOrder != SortOrder.Descending;
        }
    }
}
