namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Materialization;
    using xofz.UI;

    public sealed class LogEditorPresenter : Presenter
    {
        public LogEditorPresenter(
            LogEditorUi ui, 
            LogEditor editor) 
            : base(ui, null)
        {
            this.ui = ui;
            this.editor = editor;
        }

        public void Setup(Navigator navigator)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.TypeChanged += this.ui_TypeChanged;
            this.ui.AddKeyTapped += this.ui_AddKeyTapped;
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Types = new OrderedMaterializedEnumerable<string>(
                    new List<string> { "Information", "Warning", "Error", "Custom" });
                this.ui.SelectedType = "Information";
            });

            navigator.RegisterPresenter(this);
        }

        public override void Start()
        {
            UiHelpers.Write(this.ui, () => this.ui.Display());
        }

        public override void Stop()
        {
            UiHelpers.Write(this.ui, () => this.ui.Hide());
        }

        private void ui_TypeChanged()
        {
            var customSelected = UiHelpers.Read(this.ui, () => this.ui.SelectedType) == "Custom";
            UiHelpers.Write(this.ui, () => this.ui.CustomTypeVisible = customSelected);
        }

        private void ui_AddKeyTapped()
        {
            var customSelected = UiHelpers.Read(this.ui, () => this.ui.SelectedType) == "Custom";
            var type = customSelected
                ? UiHelpers.Read(this.ui, () => this.ui.CustomType)
                : UiHelpers.Read(this.ui, () => this.ui.SelectedType);

            this.editor.AddEntry(
                type,
                UiHelpers.Read(this.ui, () => this.ui.Content));

            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Content = null;
                this.ui.SelectedType = "Information";
                this.ui.Hide();
            });
        }

        private int setupIf1;
        private readonly LogEditorUi ui;
        private readonly LogEditor editor;
    }
}
