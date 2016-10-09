namespace xofz.UI.WPF
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Controls;
    using xofz.UI.WPF.Internal;

    public class PageUi : Page, Ui
    {
        public PageUi()
        {
            this.root = new DispatcherSynchronizeInvoke(this.Dispatcher);
            this.writeFinished = new AutoResetEvent(false);
        }

        ISynchronizeInvoke Ui.Root => this.root;

        AutoResetEvent Ui.WriteFinished => this.writeFinished;

        public MarshalByRefObject Referrer => null;

        void Ui.AssertStability()
        {
            // how do I implement this?
        }

        private readonly ISynchronizeInvoke root;
        private readonly AutoResetEvent writeFinished;
    }
}
