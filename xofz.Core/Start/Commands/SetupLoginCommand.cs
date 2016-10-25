namespace xofz.Start.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Start;
    using xofz.UI;

    public class SetupLoginCommand : Command
    {
        public SetupLoginCommand(
            LoginUi ui,
            AccessController accessController,
            Navigator navigator)
        {
            this.ui = ui;
            this.accessController = accessController;
            this.navigator = navigator;
        }

        public override void Execute()
        {
            new LoginPresenter(
                    this.ui,
                    new xofz.Framework.Timer(),
                    this.accessController)
                .Setup(this.navigator);
        }

        private readonly LoginUi ui;
        private readonly AccessController accessController;
        private readonly Navigator navigator;
    }
}
