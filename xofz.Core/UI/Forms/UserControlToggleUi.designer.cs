﻿namespace xofz.UI.Forms
{
    partial class UserControlToggleUi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.key = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // key
            // 
            this.key.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.key.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.key.Location = new System.Drawing.Point(0, 0);
            this.key.Margin = new System.Windows.Forms.Padding(0);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(100, 94);
            this.key.TabIndex = 3;
            this.key.Text = "Sample Label";
            this.key.UseVisualStyleBackColor = true;
            this.key.Click += new System.EventHandler(this.key_Click);
            // 
            // UserControlToggleUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.key);
            this.Name = "UserControlToggleUi";
            this.Size = new System.Drawing.Size(100, 94);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button key;
    }
}
