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
            Navigator navigator,
            int loginDurationMinutes = 15)
        {
            this.ui = ui;
            this.accessController = accessController;
            this.navigator = navigator;
            this.loginDurationMinutes = loginDurationMinutes;
        }

        public override void Execute()
        {
            new LoginPresenter(
                this.ui,
                new xofz.Framework.Timer(),
                this.accessController)
                .Setup(
                    this.navigator,
                    this.loginDurationMinutes);
        }

        private readonly LoginUi ui;
        private readonly AccessController accessController;
        private readonly Navigator navigator;
        private readonly int loginDurationMinutes;
    }
}
