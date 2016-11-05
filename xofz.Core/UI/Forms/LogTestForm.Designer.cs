namespace xofz.UI.Forms
{
    partial class LogTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logUi = new xofz.UI.Forms.UserControlLogUi();
            this.SuspendLayout();
            // 
            // logUi
            // 
            this.logUi.Location = new System.Drawing.Point(12, 12);
            this.logUi.Name = "logUi";
            this.logUi.Size = new System.Drawing.Size(860, 603);
            this.logUi.TabIndex = 0;
            // 
            // LogTestForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.logUi);
            this.Name = "LogTestForm";
            this.Text = "LogTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlLogUi logUi;
    }
}