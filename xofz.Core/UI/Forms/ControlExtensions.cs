namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public static class ControlExtensions
    {
        public static void SafeReplace(this Control control, Control container)
        {
            if (control == null)
            {
                return;
            }

            if (container.Controls.Count == 1 && control.Equals(
                container.Controls[0]))
            {
                return;
            }

            container.Controls.Clear();
            container.Controls.Add(control);
        }
    }
}
