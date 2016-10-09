namespace xofz.Apps.ImageSplicer.UI
{
    using xofz.UI;

    public interface ImagesUi : Ui
    {
        MaterializedEnumerable<object> Images { get; set; }
    }
}
