namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Start;
    using xofz.Start.Commands;

    class LogTesterEntry
    {
        public void Go()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var e = new CommandExecutor();
            var n = new Navigator();
            var ac = new AccessController(new[] { "1111" });
            var shell = new LogTester();

            e.Execute(
                new SetupLogCommand(
                    new UserControlLogUi(),
                    shell,
                    ac,
                    n,
                    new FormLogEditorUi()));
            n.Present<LogPresenter>();
            Application.Run(shell);
        }
    }
}
