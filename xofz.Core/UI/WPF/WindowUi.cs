namespace xofz.UI.WPF
{
    using System.ComponentModel;
    using System.Windows;
    using xofz.UI.WPF.Internal;

    public class WindowUi : Window, Ui
    {
        public WindowUi()
        {
            this.invoker = new DispatcherSynchronizeInvoke(this.Dispatcher);
        }

        ISynchronizeInvoke Ui.Root => this.invoker;

        bool Ui.Disabled
        {
            get => !this.IsEnabled;

            set => this.IsEnabled = !value;
        }

        protected readonly ISynchronizeInvoke invoker;
    }
}
