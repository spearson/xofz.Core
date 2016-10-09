namespace xofz.UI
{
    using System;
    using System.Windows.Forms;

    public static class UiHelpers
    {
        public static void Write(Ui ui, Action writer)
        {
            if (ui.Root.InvokeRequired)
            {
                ui.Root.BeginInvoke((Action)(() =>
                {
                    writer();
                    ui.WriteFinished.Set();
                }), new object[0]);
                return;
            }

            writer();
            ui.WriteFinished.Set();
        }

        public static T Read<T>(Ui ui, Func<T> valueReader)
        {
            var value = default(T);
            if (ui.Root.InvokeRequired)
            {
                ui.Root.Invoke((MethodInvoker)(() => value = valueReader()), new object[0]);
                return value;
            }

            return valueReader();
        }
    }
}
