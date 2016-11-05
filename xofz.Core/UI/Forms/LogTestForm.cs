namespace xofz.UI.Forms
{
    using System;
    using System.Windows.Forms;
    using xofz.Framework.Implementation;
    using xofz.Presentation;

    public class Tester
    {
        public void Go()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogTestForm());
        }
    }

    public partial class LogTestForm : FormUi, ShellUi
    {
        public LogTestForm()
        {
            this.InitializeComponent();
            var log = new TextFileLog("Test log.txt");

            for (var i = 0; i < 100; ++i)
            {
                log.AddEntry("Test entry", new[] { Guid.NewGuid().ToString() });
            }

            new LogPresenter(
                this.logUi,
                this,
                log).Setup();
        }

        public void SwitchUi(Ui newUi)
        {
        }
    }
}
