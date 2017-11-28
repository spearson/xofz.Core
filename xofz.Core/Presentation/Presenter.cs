﻿namespace xofz.Presentation
{
    using UI;

    public class Presenter
    {
        public Presenter(Ui ui, ShellUi shell)
        {
            this.ui = ui;
            this.shell = shell;
        }

        public virtual void Start()
        {
            var s = this.shell;
            UiHelpers.Write(s, () => s.SwitchUi(this.ui));
            s.WriteFinished.WaitOne();
        }

        public virtual void Stop()
        {
        }

        private readonly Ui ui;
        private readonly ShellUi shell;
    }
}
