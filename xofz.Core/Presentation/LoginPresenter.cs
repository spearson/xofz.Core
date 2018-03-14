namespace xofz.Presentation
{
    using System;
    using System.Text;
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
            TimeSpan loginDuration)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.loginDuration = loginDuration;
            var w = this.web;
            this.ui.LoginKeyTapped += this.ui_LoginKeyTapped;
            this.ui.CancelKeyTapped += this.Stop;
            this.ui.BackspaceKeyTapped += this.ui_BackspaceKeyTapped;
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.TimeRemaining = "Not logged in";
                });

            w.Run<AccessController>(ac =>
            {
                var cal = ac.CurrentAccessLevel;
                UiHelpers.Write(
                    this.ui,
                    () => this.ui.CurrentAccessLevel = cal);
                ac.AccessLevelChanged += this.accessLevelChanged;
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
            this.currentPassword = UiHelpers.Read(
                this.ui,
                () => this.ui.CurrentPassword);
            UiHelpers.Write(this.ui, this.ui.Display);
            this.ui.WriteFinished.WaitOne();
        }

        public override void Stop()
        {
            var w = this.web;
            var cp = this.currentPassword;
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.CurrentPassword = cp;
                this.ui.Hide();
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<LatchHolder>(
                latch => latch.Latch.Set(),
                "LoginLatch");
        }

        private void ui_BackspaceKeyTapped()
        {
            var cPw = UiHelpers.Read(
                this.ui,
                () => this.ui.CurrentPassword);
            var newPw = StringHelpers.RemoveEndChars(
                cPw,
                1);
            UiHelpers.Write(this.ui,
                () =>
                {
                    this.ui.CurrentPassword = newPw;
                    this.ui.FocusPassword();
                });
        }

        private void ui_LoginKeyTapped()
        {
            var password = UiHelpers.Read(
                this.ui, () => this.ui.CurrentPassword);
            var w = this.web;
            var newCal = AccessLevel.None;
            w.Run<AccessController>(
                ac =>
                {
                    var previousCal = ac.CurrentAccessLevel;
                    ac.InputPassword(
                        password,
                        this.loginDuration);
                    newCal = ac.CurrentAccessLevel;
                    if (previousCal == newCal)
                    {
                        w.Run<xofz.Framework.Timer, EventRaiser>(
                            (t, er) =>
                            {
                                er.Raise(t, nameof(t.Elapsed));
                            },
                            "LoginTimer");
                    }
                });

            if (newCal > AccessLevel.None)
            {
                this.setCurrentPassword(password);
                this.Stop();
                return;
            }

            UiHelpers.Write(
                this.ui,
                () => this.ui.FocusPassword());
        }

        private void setCurrentPassword(string oldPassword)
        {
            this.currentPassword = oldPassword;
        }

        private void timer_Elapsed()
        {
            var h = this.timerHandlerFinished;
            h.Reset();

            var w = this.web;
            w.Run<AccessController>(ac =>
            {
                var cal = ac.CurrentAccessLevel;
                string timeRemaining;
                if (cal > AccessLevel.None)
                {
                    var tr = ac.TimeRemaining;
                    var sb = new StringBuilder();
                    sb.Append(
                        (int)tr.TotalHours);
                    sb.Append(':');
                    sb.Append(tr.Minutes);
                    sb.Append(':');
                    sb.Append(tr.Seconds);
                    sb.Append('.');
                    sb.Append(tr.Milliseconds);
                    timeRemaining = sb.ToString();
                }
                else
                {
                    timeRemaining = "Not logged in";
                }

                var noAccess = cal == AccessLevel.None;
                if (noAccess)
                {
                    this.setCurrentPassword(null);
                }

                UiHelpers.Write(this.ui,
                    () =>
                    {
                        this.ui.CurrentAccessLevel = cal;
                        this.ui.TimeRemaining = timeRemaining;
                    });
            });

            h.Set();
        }

        private void accessLevelChanged(AccessLevel newAccessLevel)
        {
            var w = this.web;
            if (newAccessLevel == AccessLevel.None)
            {
                UiHelpers.Write(
                    this.ui,
                    () => this.ui.CurrentPassword = null);
            }

            w.Run<xofz.Framework.Timer, EventRaiser>(
                (t, er) =>
                {
                    er.Raise(
                        t,
                        nameof(t.Elapsed));
                },
                "LoginTimer");
        }

        private int setupIf1;
        private string currentPassword;
        private TimeSpan loginDuration;
        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
