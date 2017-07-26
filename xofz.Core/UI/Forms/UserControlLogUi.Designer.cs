namespace xofz.UI.Forms
{
    partial class UserControlLogUi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.startDatePicker = new System.Windows.Forms.MonthCalendar();
            this.endDatePicker = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.entriesGrid = new System.Windows.Forms.DataGridView();
            this.timestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addKey = new System.Windows.Forms.Button();
            this.downKey = new System.Windows.Forms.Button();
            this.upKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(0, 13);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 0;
            this.startDatePicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.startDatePicker_DateChanged);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endDatePicker.Location = new System.Drawing.Point(373, 13);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 1;
            this.endDatePicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.endDatePicker_DateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "End:";
            // 
            // entriesGrid
            // 
            this.entriesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entriesGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.entriesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timestampColumn,
            this.typeColumn,
            this.contentColumn});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.entriesGrid.Location = new System.Drawing.Point(0, 175);
            this.entriesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.entriesGrid.Name = "entriesGrid";
            this.entriesGrid.ReadOnly = true;
            this.entriesGrid.RowHeadersVisible = false;
            this.entriesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entriesGrid.Size = new System.Drawing.Size(600, 225);
            this.entriesGrid.TabIndex = 4;
            // 
            // timestampColumn
            // 
            this.timestampColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.timestampColumn.HeaderText = "Timestamp";
            this.timestampColumn.Name = "timestampColumn";
            this.timestampColumn.ReadOnly = true;
            this.timestampColumn.Width = 165;
            // 
            // typeColumn
            // 
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Width = 135;
            // 
            // contentColumn
            // 
            this.contentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.contentColumn.HeaderText = "Content";
            this.contentColumn.Name = "contentColumn";
            this.contentColumn.ReadOnly = true;
            // 
            // addKey
            // 
            this.addKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addKey.AutoSize = true;
            this.addKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.addKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.addKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addKey.Location = new System.Drawing.Point(227, 143);
            this.addKey.Margin = new System.Windows.Forms.Padding(0);
            this.addKey.Name = "addKey";
            this.addKey.Size = new System.Drawing.Size(100, 32);
            this.addKey.TabIndex = 5;
            this.addKey.Text = "Add Entry";
            this.addKey.UseVisualStyleBackColor = true;
            this.addKey.Click += new System.EventHandler(this.addKey_Click);
            // 
            // downKey
            // 
            this.downKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downKey.AutoSize = true;
            this.downKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.downKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.downKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downKey.Location = new System.Drawing.Point(277, 77);
            this.downKey.Margin = new System.Windows.Forms.Padding(0);
            this.downKey.Name = "downKey";
            this.downKey.Size = new System.Drawing.Size(50, 54);
            this.downKey.TabIndex = 6;
            this.downKey.Text = "↓";
            this.downKey.UseVisualStyleBackColor = true;
            this.downKey.Click += new System.EventHandler(this.downKey_Click);
            // 
            // upKey
            // 
            this.upKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upKey.AutoSize = true;
            this.upKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.upKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.upKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.upKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upKey.Location = new System.Drawing.Point(277, 13);
            this.upKey.Margin = new System.Windows.Forms.Padding(0);
            this.upKey.Name = "upKey";
            this.upKey.Size = new System.Drawing.Size(50, 54);
            this.upKey.TabIndex = 7;
            this.upKey.Text = "↑";
            this.upKey.UseVisualStyleBackColor = true;
            this.upKey.Click += new System.EventHandler(this.upKey_Click);
            // 
            // UserControlLogUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.upKey);
            this.Controls.Add(this.downKey);
            this.Controls.Add(this.addKey);
            this.Controls.Add(this.entriesGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Name = "UserControlLogUi";
            this.Size = new System.Drawing.Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar startDatePicker;
        private System.Windows.Forms.MonthCalendar endDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView entriesGrid;
        private System.Windows.Forms.Button addKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentColumn;
        private System.Windows.Forms.Button downKey;
        private System.Windows.Forms.Button upKey;
    }
}
