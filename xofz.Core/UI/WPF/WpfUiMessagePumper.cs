namespace xofz.UI.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public sealed class WpfUiMessagePumper 
        : UiMessagePumper
    {
        void UiMessagePumper.Pump()
        {
            Application.Current?.Dispatcher?.Invoke(
                DispatcherPriority.Send,
                (Action) (() => { }));
        }
    }
}
