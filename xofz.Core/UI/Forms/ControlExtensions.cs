namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public static class ControlExtensions
    {
        public static void SafeReplace(
            this Control control, 
            Control container)
        {
            ControlHelpers.SafeReplaceV2(
                control,
                container);
        }
    }
}
