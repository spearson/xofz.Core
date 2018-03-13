namespace xofz.Presentation
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public class LoginPresenter : Presenter
    {
        public LoginPresenter(
            LoginUi ui,
            MethodWeb web)
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
            this.timerHandlerFinished = new ManualResetEvent(true);
        }

        public virtual void Setup(
            int loginDurationMinutes)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.loginDurationMinutes = loginDurationMinutes;

            var w = this.web;
            this.ui.LoginKeyTapped += this.ui_LoginKeyTapped;
            this.ui.CancelKeyTapped += this.Stop;
            this.ui.BackspaceKeyTapped += this.ui_BackspaceKeyTapped;
            this.ui.LogOutKeyTapped += this.ui_LogOutKeyTapped;
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.LogOutKeyEnabled = false;
                    this.ui.TimeRemaining = "Not logged in";
                });

            w.Run<xofz.Framework.Timer, Navigator>((t, n) =>
                {
                    t.Elapsed += this.timer_Elapsed;
                    t.Start(1000);
                    n.RegisterPresenter(this);
                },
                "LoginTimer");
        }

        public override void Start()
        {
            var w = this.web;
            this.oldPassword = UiHelpers.Read(this.ui, () => this.ui.CurrentPassword);
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Display();
            });
            this.ui.WriteFinished.WaitOne();
        }

        public override void Stop()
        {
            var w = this.web;
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.CurrentPassword = this.oldPassword;
                this.ui.Hide();
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<LatchHolder>(
                latch => latch.Latch.Set(),
                "LoginLatch");
        }

        private void ui_BackspaceKeyTapped()
        {
            UiHelpers.Write(this.ui,
                () => this.ui.CurrentPassword = StringHelpers.RemoveEndChars(this.ui.CurrentPassword, 1));
        }

        private void ui_LogOutKeyTapped()
        {
            var w = this.web;
            w.Run<AccessController>(
                ac => ac.InputPassword(null));
            this.setOldPassword(null);
            this.Stop();
        }

        private void ui_LoginKeyTapped()
        {
            var password = UiHelpers.Read(
                this.ui, () => this.ui.CurrentPassword);
            var w = this.web;
            w.Run<AccessController>(
                ac => ac.InputPassword(password));
            var cal = this.web.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);

            if (cal == AccessLevel.None)
            {
                // show login failed message
                this.setOldPassword(null);
                w.Run<xofz.Framework.Timer, EventRaiser>(
                    (t, er) =>
                    {
                        er.Raise(t, nameof(t.Elapsed));
                    },
                    "LoginTimer");
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
            var h = this.timerHandlerFinished;
            h.Reset();
            var elapsed = this.elapsedSeconds + 1;
            this.setElapsedSeconds(elapsed);
            var w = this.web;
            long duration = 0;
            w.Run<AccessController>(ac =>
            {
                var cal = ac.CurrentAccessLevel;
                duration = this.loginDurationMinutes * 60;
                UiHelpers.Write(this.ui,
                    () =>
                    {
                        this.ui.CurrentAccessLevel = cal;
                        this.ui.TimeRemaining = cal > AccessLevel.None
                            ? TimeSpan.FromSeconds(duration - elapsed).ToString()
                            : "Not logged in";
                        this.ui.LogOutKeyEnabled = cal > AccessLevel.None;
                    });
            });

            if (elapsed != duration)
            {
                h.Set();
                return;
            }

            UiHelpers.Write(
                this.ui,
                () => this.ui.CurrentPassword = null);
            this.ui.WriteFinished.WaitOne();
            this.setOldPassword(null);
            this.setElapsedSeconds(0);
            h.Set();
        }

        private int setupIf1;
        private int loginDurationMinutes;
        private long elapsedSeconds;
        private string oldPassword;
        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
