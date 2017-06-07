﻿namespace xofz.Presentation
{
    using xofz.UI;

    public class PopupNamedPresenter : NamedPresenter
    {
        public PopupNamedPresenter(
            PopupUi ui) 
            : base(ui, null)
        {
            this.ui = ui;
        }

        public override string Name { get; set; }

        public override void Start()
        {
            UiHelpers.Write(this.ui, () => this.ui.Display());
            this.ui.WriteFinished.WaitOne();
        }

        public override void Stop()
        {
            UiHelpers.Write(this.ui, () => this.ui.Hide());
            this.ui.WriteFinished.WaitOne();
        }

        private readonly PopupUi ui;
    }
}
