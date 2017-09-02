// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SortableBindingList.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace xofz.UI.Forms.Internal
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    internal sealed class SortableBindingList<T> : BindingList<T>
    {
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (prop.PropertyType.GetInterface("IComparable") == null)
            {
                return;
            }

            var items = this.Items.ToList();
            items.Sort(
                (a, b) =>
                    {
                        var aVal = prop.GetValue(a) as IComparable;
                        var bVal = prop.GetValue(b) as IComparable;
                        return aVal.CompareTo(bVal) * (direction == ListSortDirection.Ascending ? 1 : -1);
                    });

            this.Items.Clear();
            foreach (var i in items)
            {
                this.Items.Add(i);
            }
        }
    }
}
