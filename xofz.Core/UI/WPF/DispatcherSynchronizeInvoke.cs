namespace xofz.UI.WPF
{
    using System;
    using System.ComponentModel;
    using System.Windows.Threading;

    public class DispatcherSynchronizeInvoke 
        : ISynchronizeInvoke
    {
        public DispatcherSynchronizeInvoke(
            Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        bool ISynchronizeInvoke.InvokeRequired => 
            !this.dispatcher?.CheckAccess() ?? false;

        IAsyncResult ISynchronizeInvoke.BeginInvoke(
            Delegate method, 
            object[] args)
        {
            this.dispatcher?.BeginInvoke(
                method, 
                args);

            return default;
        }

        object ISynchronizeInvoke.EndInvoke(
            IAsyncResult result)
        {
            return null;
        }

        object ISynchronizeInvoke.Invoke(
            Delegate method, 
            object[] args)
        {
            return this.dispatcher?.Invoke(method, args);
        }

        protected readonly Dispatcher dispatcher;
    }
}
