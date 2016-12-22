namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLoginCommand : Command
    {
        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            int loginDurationMinutes = 15)
        {
            this.ui = ui;
            this.web = web;
            this.loginDurationMinutes = loginDurationMinutes;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new LoginPresenter(
                this.ui,
                this.web)
                .Setup(
                    this.loginDurationMinutes);
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                this.ui);
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "LoginTimer");
        }

        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly int loginDurationMinutes;
    }
}
