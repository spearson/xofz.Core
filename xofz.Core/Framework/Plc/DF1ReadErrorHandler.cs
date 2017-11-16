namespace xofz.Framework.Plc
{
    using System;
    using xofz.Framework;
    using xofz.UI;

    public class DF1ReadErrorHandler
    {
        public DF1ReadErrorHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            string dataName,
            string dataLocation,
            Exception ex,
            Action cleanup = default(Action))
        {
            var w = this.web;
            cleanup?.Invoke();

            w.Run<Messenger>(m =>
            {
                var message =
                    "Error reading " + dataName +
                    " location " + dataLocation + "." + Environment.NewLine +
                    ex.GetType() + Environment.NewLine + ex.Message;
                UiHelpers.Write(
                    m.Subscriber,
                    () => m.GiveError(message));
                m.Subscriber.WriteFinished.WaitOne();
            });
        }

        private readonly MethodWeb web;
    }
}
