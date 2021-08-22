namespace xofz.UI.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public class WpfUiMessagePumper 
        : UiMessagePumper
    {
        public virtual void Pump()
        {
            Application.Current?.Dispatcher?.Invoke(
                this.Priority,
                (Action) (() => { }));
        }

        public virtual DispatcherPriority Priority { get; set; }
            = DispatcherPriority.Send;
    }
}
