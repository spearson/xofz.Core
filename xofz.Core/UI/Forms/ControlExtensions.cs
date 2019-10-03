﻿namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public static class ControlExtensions
    {
        public static void SafeReplace(
            this Control control, 
            Control container)
        {
            if (control == null)
            {
                return;
            }

            if (container == null)
            {
                return;
            }

            var controls = container.Controls;
            if (controls.Count == 1 && 
                ReferenceEquals(control, controls[0]))
            {
                return;
            }

            controls.Clear();
            controls.Add(control);
        }
    }
}
