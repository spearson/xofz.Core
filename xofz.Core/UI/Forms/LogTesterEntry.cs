namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Root;
    using xofz.Root.Commands;

    class LogTesterEntry
    {
        public void Go()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var e = new CommandExecutor();
            var ac = new AccessController(new[] { "1111" });
            var shell = new LogTester();
            var w = new MethodWeb();
            w.RegisterDependency(new Navigator());

            e.Execute(
                new SetupLogCommand(
                    new UserControlLogUi(),
                    shell,
                    new FormLogEditorUi(shell),
                    w));
            w.Run<Navigator>(n => n.Present<LogPresenter>());
            Application.Run(shell);
        }
    }
}
