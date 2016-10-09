namespace xofz.UI.WPF
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using xofz.UI.WPF.Internal;

    public class WindowUi : Window, Ui
    {
        public WindowUi()
        {
            this.root = new DispatcherSynchronizeInvoke(this.Dispatcher);
            this.writeFinished = new AutoResetEvent(false);
        }

        ISynchronizeInvoke Ui.Root => this.root;

        AutoResetEvent Ui.WriteFinished => this.writeFinished;

        MarshalByRefObject Ui.Referrer => null;

        void Ui.AssertStability()
        {
            // how to implement?
        }

        private readonly ISynchronizeInvoke root;
        private readonly AutoResetEvent writeFinished;
    }
}
