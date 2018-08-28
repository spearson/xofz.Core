namespace xofz.UI.WPF
{
    using System.ComponentModel;
    using System.Windows.Controls;
    using xofz.UI.WPF.Internal;

    public class UserControlUi : UserControl, Ui
    {
        public UserControlUi()
        {
            this.root = new DispatcherSynchronizeInvoke(this.Dispatcher);
        }

        ISynchronizeInvoke Ui.Root => this.root;
        
        bool Ui.Disabled
        {
            get => !this.IsEnabled;

            set => this.IsEnabled = !value;
        }

        private readonly ISynchronizeInvoke root;
    }
}
