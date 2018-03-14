namespace xofz.Framework
{
    using System;
    using xofz.Framework.Internal;

    public class Timer : IDisposable
    {
        public Timer()
        {
            this.autoReset = true;
            this.locker = new object();
        }

        public virtual event Action Elapsed;

        public virtual bool AutoReset
        {
            get => this.autoReset;

            set => this.autoReset = value;
        }

        public virtual void Start(TimeSpan interval)
        {
            this.Start((long)interval.TotalMilliseconds);
        }

        public virtual void Start(long intervalMilliseconds)
        {
            lock (this.locker)
            {
                if (this.started)
                {
                    return;
                }

                NativeMethods.CreateTimerQueueTimer(
                    out this.handle,
                    IntPtr.Zero,
                    this.ticked,
                    IntPtr.Zero,
                    (uint)intervalMilliseconds,
                    (uint)intervalMilliseconds,
                    CallbackOptions.QueueToWorkerThread);
                this.started = true;
            }
        }

        public virtual void Stop()
        {
            lock (this.locker)
            {
                if (!this.started)
                {
                    return;
                }

                NativeMethods.DeleteTimerQueueTimer(
                    IntPtr.Zero, 
                    this.handle, 
                    IntPtr.Zero);
                this.started = false;
            }
            
        }

        public virtual void Dispose()
        {
            this.Stop();
        }

        private void ticked(IntPtr parameterPointer, bool unused)
        {
            if (!this.AutoReset)
            {
                this.Stop();
            }

            this.Elapsed?.Invoke();
        }
        
        private bool started;
        private IntPtr handle;
        private volatile bool autoReset;
        private readonly object locker;
    }
}
