namespace xofz.Framework.Timers
{
    using System;
    using xofz.Framework.Timers.Internal;

    public class TimerQueueTimer 
        : Timer
    {
        public TimerQueueTimer()
        {
            this.callback = this.ticked;
        }

        public override event Do Elapsed;

        public override void Start(
            long intervalMilliseconds)
        {
            lock (this.locker)
            {
                if (this.started)
                {
                    return;
                }

                NativeMethods.CreateTimerQueueTimer(
                    out this.currentHandle,
                    IntPtr.Zero,
                    this.callback,
                    IntPtr.Zero,
                    (uint)intervalMilliseconds,
                    (uint)intervalMilliseconds,
                    CallbackOptions.QueueToWorkerThread);
                this.started = true;
            }
        }

        public override void Stop()
        {
            lock (this.locker)
            {
                if (!this.started)
                {
                    return;
                }

                NativeMethods.DeleteTimerQueueTimer(
                    IntPtr.Zero, 
                    this.currentHandle, 
                    IntPtr.Zero);
                this.started = false;
            }
        }

        public override void Dispose()
        {
            this.Stop();
        }

        protected virtual void ticked(
            IntPtr parameterPointer, 
            bool unused)
        {
            if (!this.AutoReset)
            {
                this.Stop();
            }

            this.Elapsed?.Invoke();
        }

        protected IntPtr currentHandle;
        private readonly TimerCallback callback;
    }
}
