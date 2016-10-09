namespace xofz.Presentation
{
    using System.Threading;
    using xofz.UI;

    public sealed class MainPresenter : Presenter
    {
        public MainPresenter(
            MainUi ui, 
            Navigator navigator) 
            : base(ui, null)
        {
            this.ui = ui;
            this.navigator = navigator;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.ShutdownRequested += this.ui_ShutdownRequested;
        }

        private void ui_ShutdownRequested()
        {
            this.navigator.Present<ShutdownPresenter>();
        }

        private int setupIf1;
        private readonly MainUi ui;
        private readonly Navigator navigator;
    }
}
