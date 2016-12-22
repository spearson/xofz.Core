namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupMainCommand : Command
    {
        public SetupMainCommand(
            MainUi ui,
            MethodWeb web)
        {
            this.ui = ui;
            this.web = web;
        }

        public override void Execute()
        {
            new MainPresenter(
                this.ui,
                this.web)
                .Setup();
        }

        private readonly MainUi ui;
        private readonly MethodWeb web;
    }
}
