namespace xofz.Apps.ImageSplicer.Presenters
{
    using Presentation;
    using xofz.Apps.ImageSplicer.Framework;
    using xofz.Apps.ImageSplicer.UI;
    using xofz.Framework.Transformation;
    using xofz.UI;

    public sealed class ImagesPresenter : Presenter
    {
        public ImagesPresenter(
            ImagesUi ui,
            ShellUi shell,
            ImageReader reader,
            EnumerableSplicer splicer)
            : base(ui, shell)
        {
            this.ui = ui;
            this.reader = reader;
            this.splicer = splicer;
        }

        public void Start(string location)
        {
            this.ui.Images = this.splicer.Splice(this.reader.Read(location));
            base.Start();
        }

        private readonly ImagesUi ui;
        private readonly ImageReader reader;
        private readonly EnumerableSplicer splicer;
    }
}
