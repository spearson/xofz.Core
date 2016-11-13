﻿namespace xofz.Start.Commands
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
            AccessController accessController,
            Navigator navigator,
            LogEditorUi editorUi,
            string filePath = @"Log.log",
            AccessLevel editLevel = AccessLevel.None)
        {
            this.ui = ui;
            this.shell = shell;
            this.accessController = accessController;
            this.navigator = navigator;
            this.editorUi = editorUi;
            this.filePath = filePath;
            this.editLevel = editLevel;
        }

        public virtual Log Log => this.log;

        public virtual LogEditor Editor => this.editor;

        public override void Execute()
        {
            var l = new TextFileLog(this.filePath);
            this.setLog(l);
            this.setEditor(l);
            var n = this.navigator;

            new LogPresenter(
                this.ui,
                this.shell,
                l,
                new xofz.Framework.Timer(),
                this.accessController,
                n)
                .Setup(
                    this.editLevel);

            new LogEditorPresenter(
                this.editorUi,
                l)
                .Setup(n);
        }

        private void setLog(Log log)
        {
            this.log = log;
        }

        private void setEditor(LogEditor editor)
        {
            this.editor = editor;
        }

        private Log log;
        private LogEditor editor;
        private readonly LogUi ui;
        private readonly ShellUi shell;
        private readonly AccessController accessController;
        private readonly Navigator navigator;
        private readonly LogEditorUi editorUi;
        private readonly string filePath;
        private readonly AccessLevel editLevel;
    }
}
