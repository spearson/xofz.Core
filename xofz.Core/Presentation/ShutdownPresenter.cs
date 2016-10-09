namespace xofz.Presentation
{
    using System;
    using System.Diagnostics;
    using UI;

    public sealed class ShutdownPresenter : Presenter
    {
        public ShutdownPresenter(
            Ui mainUi, 
            Action cleanup) 
            : base(mainUi, null)
        {
            this.mainUi = mainUi;
            this.cleanup = cleanup;
        }

        public override void Start()
        {
            UiHelpers.Write(this.mainUi, () => this.cleanup());
            this.mainUi.WriteFinished.WaitOne();
            Process.GetCurrentProcess().Kill();
        }

        private readonly Ui mainUi;
        private readonly Action cleanup;
    }
}
