namespace xofz.Apps.Connector.Presenters
{
    using Presentation;
    using xofz.Apps.Connector.Framework;
    using xofz.Apps.Connector.UI;
    using xofz.Framework.Transformation;
    using xofz.UI;

    public sealed class ConnectionPresenter : Presenter
    {
        public ConnectionPresenter(
            ConnectionUi ui, 
            ShellUi shell,
            Reader reader, 
            EnumerableConnector connector)
            :base(ui, shell)
        {
            this.ui = ui;
            this.reader = reader;
            this.connector = connector;
        }

        public void Start(string location)
        {
            this.ui.Connection = this.connector.Connect(this.reader.Read(location));
            base.Start();
        }

        private readonly ConnectionUi ui;
        private readonly Reader reader;
        private readonly EnumerableConnector connector;
    }
}
