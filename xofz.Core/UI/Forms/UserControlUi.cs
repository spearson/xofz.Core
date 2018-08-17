﻿namespace xofz.UI.Forms
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    public class UserControlUi : UserControl, Ui
    {
        public UserControlUi()
        {
            this.writeFinished = new AutoResetEvent(false);
        }

        ISynchronizeInvoke Ui.Root => this;

        AutoResetEvent Ui.WriteFinished => this.writeFinished;

        MarshalByRefObject Ui.Referrer => this.Parent;

        bool Ui.Disabled
        {
            get => !this.Enabled;

            set => this.Enabled = !value;
        }

        void Ui.AssertStability()
        {
            var stable = true;
            this.Invoke((Action)(() =>
            {
                var fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (!field.FieldType.IsAssignableFrom(typeof(ISynchronizeInvoke)))
                    {
                        continue;
                    }

                    var value = (ISynchronizeInvoke)field.GetValue(this);
                    if (value.InvokeRequired)
                    {
                        stable = false;
                    }
                }
            }));

            if (!stable)
            {
                throw new InvalidOperationException("Unstable UI fields in " + this.GetType());
            }
        }

        private readonly AutoResetEvent writeFinished;
    }
}
