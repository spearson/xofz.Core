namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    using xofz.Framework;
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

            var n = new Navigator();
            new LogPresenter(
                this.logUi,
                this,
                log,
                new xofz.Framework.Timer(),
                new AccessController(new[] {"1111"}),
                n).Setup();

            this.editorUi = new FormLogEditorUi();
            new LogEditorPresenter(
                this.editorUi,
                log)
                .Setup(n);
        }

        public void SwitchUi(Ui newUi)
        {
        }

        private LogEditorUi editorUi;
    }
}
