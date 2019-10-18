namespace xofz.Misc
{
    using System.Threading;

    public class Delayer
    {
        public Delayer()
            : this(ThreadPriority.AboveNormal)
        {
        }

        public Delayer(
            ThreadPriority priority)
        {
            this.priority = priority;
            this.finishedSleeping = new ManualResetEvent(true);
        }

        // please have one delayer per thread
        public virtual void Delay(
            int milliseconds)
        {
            var fs = this.finishedSleeping;
            fs.Reset();
            var t = new Thread(() =>
                {
                    Thread.Sleep(milliseconds);
                    fs.Set();
                })
                { Priority = this.priority };
            ThreadPool.QueueUserWorkItem(
                state =>
                {
                    Thread.Sleep(milliseconds);
                    fs.Set();
                });
            t.Start();
            fs.WaitOne(milliseconds);
        }

        protected readonly ThreadPriority priority;
        protected readonly ManualResetEvent finishedSleeping;
    }
}
