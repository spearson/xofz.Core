namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Implementation;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLogCommand : Command
    {
        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            MethodWeb web,
            string filePath = @"Log.log",
            AccessLevel editLevel = AccessLevel.None)
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            this.filePath = filePath;
            this.editLevel = editLevel;
        }

        public override void Execute()
        {
            this.registerDependencies();

            var w = this.web;
            new LogPresenter(
                    this.ui,
                    this.shell,
                    w)
                .Setup(
                    this.editLevel);

            new LogEditorPresenter(
                    this.editorUi,
                    w)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new TextFileLog(this.filePath));
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "LogTimer");
        }

        private readonly LogUi ui;
        private readonly ShellUi shell;
        private readonly LogEditorUi editorUi;
        private readonly MethodWeb web;
        private readonly string filePath;
        private readonly AccessLevel editLevel;
    }
}
