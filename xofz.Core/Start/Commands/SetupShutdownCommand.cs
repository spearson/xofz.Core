namespace xofz.Start.Commands
{
    using System;
    using Presentation;
    using UI;

    public class SetupShutdownCommand : Command
    {
        public SetupShutdownCommand(
            Ui mainUi,
            Navigator navigator,
            Action cleanup)
        {
            this.mainUi = mainUi;
            this.navigator = navigator;
            this.cleanup = cleanup;
        }

        public override void Execute()
        {
            new ShutdownPresenter(
                this.mainUi,
                this.cleanup)
                .Setup(
                    this.navigator);
        }

        private readonly Ui mainUi;
        private readonly Navigator navigator;
        private readonly Action cleanup;
    }
}
