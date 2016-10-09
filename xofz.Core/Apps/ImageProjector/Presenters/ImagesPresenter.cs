namespace xofz.Apps.ImageProjector.Presenters
{
    using Presentation;
    using xofz.Apps.ImageProjector.Framework;
    using xofz.Apps.ImageProjector.UI;
    using xofz.Framework.Transformation;
    using xofz.UI;

    public sealed class ImagesPresenter : Presenter
    {
        public ImagesPresenter(
            ImagesUi2D ui2D,
            ImagesUi3D ui3D,
            ImagesUi4D ui4D,
            ShellUi shell,
            ImageReader reader,
            EnumerableProjector projector)
            : base(ui2D, shell)
        {
            this.ui2D = ui2D;
            this.ui3D = ui3D;
            this.ui4D = ui4D;
            this.reader = reader;
            this.shell = shell;
            this.projector = projector;
        }

        public void Start(string imagesLocation, int dimensions)
        {
            var images = this.reader.Read(imagesLocation);
            switch (dimensions)
            {
                case 2:
                    this.ui2D.Images = this.projector.Project2(images);
                    this.shell.SwitchUi(this.ui2D);
                    return;
                case 3:
                    this.ui3D.Images = this.projector.Project3(images);
                    this.shell.SwitchUi(this.ui3D);
                    return;
                case 4:
                    this.ui4D.Images = this.projector.Project4(images);
                    this.shell.SwitchUi(this.ui4D);
                    return;
            }
        }

        private readonly ImagesUi2D ui2D;
        private readonly ImagesUi3D ui3D;
        private readonly ImagesUi4D ui4D;
        private readonly ShellUi shell;
        private readonly ImageReader reader;
        private readonly EnumerableProjector projector;
    }
}
