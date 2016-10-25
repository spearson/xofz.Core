namespace xofz.Start.Commands
{
    using xofz.Presentation;
    using xofz.Start;
    using xofz.UI;

    public class SetupMainCommand : Command
    {
        public SetupMainCommand(
            MainUi ui,
            Navigator navigator)
        {
            this.ui = ui;
            this.navigator = navigator;
        }

        public override void Execute()
        {
            new MainPresenter(
                    this.ui,
                    this.navigator)
                .Setup();
        }

        private readonly MainUi ui;
        private readonly Navigator navigator;
    }
}
