namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.Framework.Materialization;

    public partial class FormLogEditorUi : FormUi, LogEditorUi
    {
        public FormLogEditorUi()
        {
            this.InitializeComponent();
            this.FormClosing += this.this_FormClosing;

            var h = this.Handle;
        }

        void PopupUi.Display()
        {
            this.Visible = true;
        }

        public event Action AddKeyTapped;

        public event Action TypeChanged;

        public MaterializedEnumerable<string> Types
        {
            get
            {
                var ll = new LinkedList<string>();
                foreach (string item in this.entryTypeComboBox.Items)
                {
                    ll.AddLast(item);
                }

                return new LinkedListMaterializedEnumerable<string>(ll);
            }

            set
            {
                this.entryTypeComboBox.Items.Clear();
                foreach (var type in value)
                {
                    this.entryTypeComboBox.Items.Add(type);
                }
            }
        }

        string LogEditorUi.SelectedType
        {
            get { return this.entryTypeComboBox.Text; }

            set { this.entryTypeComboBox.Text = value; }
        }

        string LogEditorUi.CustomType
        {
            get { return this.customTypeTextBox.Text; }

            set { this.customTypeTextBox.Text = value; }
        }

        bool LogEditorUi.CustomTypeVisible
        {
            get { return this.customTypeTextBox.Visible; }

            set { this.customTypeTextBox.Visible = value; }
        }

        public MaterializedEnumerable<string> Content
        {
            get
            {
                var ctb = this.contentTextBox;
                if (ctb.Lines == null)
                {
                    return new LinkedListMaterializedEnumerable<string>();
                }

                var ll = new LinkedList<string>();
                foreach (var line in ctb.Lines)
                {
                    ll.AddLast(line);
                }

                return new LinkedListMaterializedEnumerable<string>(ll);
            }

            set
            {
                this.contentTextBox.Clear();
                if (value == null)
                {
                    return;
                }

                this.contentTextBox.Lines = value.ToArray();
            }
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void entryTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Thread(() => this.TypeChanged?.Invoke()).Start();
        }

        private void addKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.AddKeyTapped?.Invoke()).Start();
        }
    }
}
