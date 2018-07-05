﻿namespace xofz.Tests.Presentation
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class LoginPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LoginUi>();
                this.web = new MethodWeb();
                this.presenter = new LoginPresenter(this.ui, this.web);
            }

            protected readonly LoginUi ui;
            protected readonly MethodWeb web;
            protected readonly LoginPresenter presenter;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Sets_time_remaining_to_not_logged_in()
            {
                this.ui.TimeRemaining = null;

                this.presenter.Setup(
                    TimeSpan.FromMinutes(15)); // default

                Assert.Equal(
                    "Not logged in",
                    this.ui.TimeRemaining);
            }

            [Fact]
            public void Sets_ui_access_level_to_current_access_level()
            {
                var w = this.web;
                var ac = A.Fake<AccessController>();
                w.RegisterDependency(ac);
                var cal = AccessLevel.Level5;
                A.CallTo(() => ac.CurrentAccessLevel)
                    .Returns(cal);
                this.ui.CurrentAccessLevel = AccessLevel.None;

                this.presenter.Setup(
                    TimeSpan.FromMinutes(15));

                Assert.Equal(
                    cal,
                    this.ui.CurrentAccessLevel);
            }

            [Fact]
            public void Starts_the_login_timer()
            {
                var w = this.web;
                var t = A.Fake<xofz.Framework.Timer>();
                w.RegisterDependency(t, "LoginTimer");
                w.RegisterDependency(A.Fake<Navigator>());
                
                this.presenter.Setup(TimeSpan.FromMinutes(15));

                A.CallTo(() => t.Start(A<long>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_itself_with_the_Navigator()
            {
                var w = this.web;
                var n = A.Fake<Navigator>();
                w.RegisterDependency(
                    A.Fake<xofz.Framework.Timer>(),
                    "LoginTimer");
                w.RegisterDependency(n);
                var p = this.presenter;

                p.Setup(TimeSpan.FromMinutes(15));

                A.CallTo(() => n.RegisterPresenter(p))
                    .MustHaveHappened();
            }
         }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Accesses_the_ui_CurrentPassword()
            {
                // to save it in a private field
                var p = this.presenter;
                p.Setup(TimeSpan.FromMinutes(15));

                p.Start();

                A.CallTo(() => this.ui.CurrentPassword)
                    .MustHaveHappened();
            }

            [Fact]
            public void Displays_the_UI()
            {
                var p = this.presenter;
                p.Setup(TimeSpan.FromMinutes(15));

                p.Start();

                A.CallTo(() => this.ui.Display())
                    .MustHaveHappened();
            }

            [Fact]
            public void Accesses_the_WriteFinished_latch()
            {
                // to wait on the UI to finish displaying itself
                var p = this.presenter;
                p.Setup(TimeSpan.FromMinutes(15));

                p.Start();

                A.CallTo(() => this.ui.WriteFinished)
                    .MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void Hides_the_ui()
            {
                var p = this.presenter;
                p.Setup(TimeSpan.FromMinutes(15));

                p.Stop();

                A.CallTo(() => this.ui.Hide())
                    .MustHaveHappened();
            }

            [Fact]
            public void Accesses_the_login_latch()
            {
                // to set it
                var lh = A.Fake<LatchHolder>();
                var w = this.web;
                w.RegisterDependency(lh, "LoginLatch");

                var p = this.presenter;
                p.Setup(TimeSpan.FromMinutes(15));

                p.Stop();

                A.CallTo(() => lh.Latch)
                    .MustHaveHappened();
            }
        }

        public class When_the_backspace_key_is_tapped : Context
        {
            [Fact]
            public void Reads_ui_CurrentPassword()
            {
                this.presenter.Setup(TimeSpan.FromMinutes(15));

                this.ui.BackspaceKeyTapped += Raise.FreeForm<Action>.With();

                A.CallTo(() => this.ui.CurrentPassword)
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_the_ui_CurrentPassword_to_result_of_removing_1_end_char()
            {
                this.presenter.Setup(TimeSpan.FromMinutes(15));
                var f = new Fixture();
                var newPw = f.Create<string>();
                var oldPw = newPw + f.Create<char>();
                this.ui.CurrentPassword = oldPw;

                this.ui.BackspaceKeyTapped += Raise.FreeForm<Action>.With();

                Assert.Equal(newPw, this.ui.CurrentPassword);
            }

            [Fact]
            public void Focuses_the_password()
            {
                this.presenter.Setup(TimeSpan.FromMinutes(15));

                this.ui.BackspaceKeyTapped += Raise.FreeForm<Action>.With();

                A.CallTo(() => this.ui.FocusPassword())
                    .MustHaveHappened();
            }
        }
    }
}
