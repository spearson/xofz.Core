namespace xofz.Apps.ImageRotator.Configuration
{
    using xofz.Apps.ImageRotator.Framework;
    using xofz.Apps.ImageRotator.Presenters;
    using xofz.Apps.ImageRotator.UI;
    using xofz.Framework.Transformation;
    using xofz.UI;

    public class Bootstrapper
    {
        public Bootstrapper(ImagesUi ui, ImageReader reader, EnumerableRotator rotator)
        {
            this.ui = ui;
            this.reader = reader;
            this.rotator = rotator;
        }

        public virtual void Go(string imagesLocation, int rotations, ShellUi shell)
        {
            var presenter = new ImagesPresenter(
                this.ui,
                shell,
                this.reader, 
                this.rotator);
            presenter.Setup(imagesLocation);

            for (var i = 0; i < rotations; ++i)
            {
                presenter.Start();
            }
        }

        private readonly ImagesUi ui;
        private readonly ImageReader reader;
        private readonly EnumerableRotator rotator;
    }
}
