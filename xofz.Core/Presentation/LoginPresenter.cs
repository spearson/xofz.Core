namespace xofz.Presentation
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;
    using Timer = xofz.Framework.Timer;

    public class LoginPresenter : Presenter, IDisposable
    {
        public LoginPresenter(
            LoginUi ui, 
            Timer timer, 
            AccessController accessController)
            : base(ui, null)
        {
            this.ui = ui;
            this.timer = timer;
            this.accessController = accessController;
            this.timerHandlerFinished = new ManualResetEvent(true);
        }

        public virtual void Setup(
            Navigator navigator, 
            int loginDurationMinutes)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.loginDurationMinutes = loginDurationMinutes;
            this.timer.Elapsed += this.timer_Elapsed;
            this.ui.LoginKeyTapped += this.ui_LoginKeyTapped;
            this.ui.CancelKeyTapped += this.Stop;
            this.ui.BackspaceKeyTapped += this.ui_BackspaceKeyTapped;
            UiHelpers.Write(this.ui, () => this.ui.TimeRemaining = "Not logged in");
            navigator.RegisterPresenter(this);

            this.timer.Start(1000);
        }

        public override void Start()
        {
            this.oldPassword = UiHelpers.Read(this.ui, () => this.ui.CurrentPassword);
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Display();
            });
        }

        public override void Stop()
        {
            UiHelpers.Write(this.ui, () => this.ui.CurrentPassword = this.oldPassword);
            UiHelpers.Write(this.ui, () => this.ui.Hide());
            this.ui.WriteFinished.WaitOne();
        }

        public void Dispose()
        {
            this.timer.Stop();
            this.timerHandlerFinished.WaitOne();
        }

        private void ui_BackspaceKeyTapped()
        {
            UiHelpers.Write(this.ui,
                () => this.ui.CurrentPassword = StringHelpers.RemoveFromEnd(this.ui.CurrentPassword, 1));
        }

        private void ui_LoginKeyTapped()
        {
            var password = UiHelpers.Read(this.ui, () => this.ui.CurrentPassword);
            var ac = this.accessController;
            ac.InputPassword(password);
            if (ac.CurrentAccessLevel == AccessLevel.None)
            {
                // show login failed message
                this.setOldPassword(string.Empty);
                return;
            }

            this.setOldPassword(password);
            this.setElapsedSeconds(0);
            
            this.Stop();
        }

        private void setElapsedSeconds(long elapsedSeconds)
        {
            this.elapsedSeconds = elapsedSeconds;
        }

        private void setOldPassword(string oldPassword)
        {
            this.oldPassword = oldPassword;
        }

        private void timer_Elapsed()
        {
            this.timerHandlerFinished.Reset();
            var elapsed = this.elapsedSeconds + 1;
            this.setElapsedSeconds(elapsed);
            var currentAccessLevel = this.accessController.CurrentAccessLevel;
            var duration = this.loginDurationMinutes * 60;
            UiHelpers.Write(this.ui, () => this.ui.TimeRemaining = currentAccessLevel > AccessLevel.None
                ? TimeSpan.FromSeconds(duration - elapsed).ToString()
                : "Not logged in");

            if (elapsed != duration)
            {
                this.timerHandlerFinished.Set();
                return;
            }

            UiHelpers.Write(this.ui, () => this.ui.CurrentPassword = string.Empty);
            this.ui.WriteFinished.WaitOne();
            this.setOldPassword(string.Empty);
            this.setElapsedSeconds(0);
            this.timerHandlerFinished.Set();
        }

        private int setupIf1;
        private int loginDurationMinutes;
        private long elapsedSeconds;
        private string oldPassword;
        private readonly LoginUi ui;
        private readonly Timer timer;
        private readonly AccessController accessController;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
