namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public sealed class MainPresenter : Presenter
    {
        public MainPresenter(
            MainUi ui, 
            MethodWeb web) 
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.ShutdownRequested += this.ui_ShutdownRequested;
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            this.web.Run<Navigator>(n => n.Present<ShutdownPresenter>());
        }

        private int setupIf1;
        private readonly MainUi ui;
        private readonly MethodWeb web;
    }
}
