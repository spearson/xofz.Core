namespace xofz.Apps.ImageRotator.Presenters
{
    using System.Linq;
    using System.Threading;
    using Presentation;
    using xofz.Apps.ImageRotator.Framework;
    using xofz.Apps.ImageRotator.UI;
    using xofz.Framework.Transformation;
    using xofz.UI;

    public sealed class ImagesPresenter : Presenter
    {
        public ImagesPresenter(ImagesUi ui, ShellUi shell, ImageReader reader, EnumerableRotator rotator)
            : base(ui, shell)
        {
            this.ui = ui;
            this.reader = reader;
            this.rotator = rotator;
        }

        public void Setup(string imagesLocation)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.images = this.rotator.Rotate(this.reader.Read(imagesLocation), 0);
        }

        public override void Start()
        {
            this.images = this.rotator.Rotate(this.images, 1);
            this.changeUiImage();
            base.Start();
        }

        private void changeUiImage()
        {
            this.ui.CurrentImage = this.images.First();
        }

        private int setupIf1;
        private MaterializedEnumerable<object> images;
        private readonly ImagesUi ui;
        private readonly ImageReader reader;
        private readonly EnumerableRotator rotator;
    }
}
