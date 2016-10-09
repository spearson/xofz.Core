namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI;

    public partial class FormLoginUi : FormUi, LoginUi
    {
        public FormLoginUi(Form shell)
        {
            this.shell = shell;
            this.InitializeComponent();
            this.FormClosing += this.this_FormClosing;
            var h = this.Handle;
        }

        public event Action BackspaceKeyTapped;

        public event Action LoginKeyTapped;

        public event Action CancelKeyTapped;

        string LoginUi.CurrentPassword
        {
            get { return this.passwordTextBox.Text; }

            set { this.passwordTextBox.Text = value; }
        }

        public string TimeRemaining
        {
            get { return this.timeRemainingLabel.Text; }

            set { this.timeRemainingLabel.Text = value; }
        }

        void LoginUi.Display()
        {
            this.Location = new Point(this.shell.Location.X, this.shell.Location.Y);
            this.passwordTextBox.Focus();
            this.passwordTextBox.SelectAll();
            this.firstNumKeyPressed = false;
            this.Visible = true;
        }

        void LoginUi.Hide()
        {
            this.Visible = false;
        }

        private void loginKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.LoginKeyTapped?.Invoke()).Start();
        }

        private void numKey_Click(object sender, EventArgs e)
        {
            var key = (Button)sender;
            if (!this.firstNumKeyPressed)
            {
                this.passwordTextBox.Text = key.Text;
                this.firstNumKeyPressed = true;
            }
            else
            {
                this.passwordTextBox.Text += key.Text;
            }

            this.passwordTextBox.Focus();
            this.passwordTextBox.Select(this.passwordTextBox.Text.Length, 0);
        }

        private void clearKey_Click(object sender, EventArgs e)
        {
            this.passwordTextBox.Text = string.Empty;
        }

        private void backspaceKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.BackspaceKeyTapped?.Invoke()).Start();
        }

        private void cancelKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.CancelKeyTapped?.Invoke()).Start();
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            new Thread(() => this.CancelKeyTapped?.Invoke()).Start();
        }

        private bool firstNumKeyPressed;
        private readonly Form shell;
    }
}
