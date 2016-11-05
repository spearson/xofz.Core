namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    public partial class LogTester : FormUi, ShellUi
    {
        public LogTester()
        {
            this.InitializeComponent();

            var h = this.Handle;
        }

        public void SwitchUi(Ui newUi)
        {
            var control = newUi as Control;
            control.SafeReplace(this.screenPanel);

            if (newUi is UserControlLogUi)
            {
                control.Dock = DockStyle.Fill;
            }
        }
    }
}
