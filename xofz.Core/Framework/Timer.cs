// ------------------------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace xofz.Framework
{
    using System;
    using System.Threading;
    using xofz.Framework.Internal;
    using TimerCallback = xofz.Framework.Internal.TimerCallback;

    public class Timer : IDisposable
    {
        public Timer()
        {
            this.callback = this.ticked;
            this.autoReset = true;
            this.priority = ThreadPriority.Normal;
        }

        public virtual event Action Elapsed;

        public virtual bool AutoReset
        {
            get => this.autoReset;

            set => this.autoReset = value;
        }

        public virtual ThreadPriority Priority
        {
            get => this.priority;

            set
            {
                this.priority = value;
                this.threadPrioritySet = false;
            }
        }

        public virtual void Start(int intervalInMs)
        {
            if (Interlocked.CompareExchange(ref this.startedIf1, 1, 0) == 1)
            {
                return;
            }

            NativeMethods.CreateTimerQueueTimer(
                out this.handle,
                IntPtr.Zero,
                this.callback,
                IntPtr.Zero,
                (uint)intervalInMs,
                (uint)intervalInMs,
                CallbackOptions.ExecuteInTimerThread);
        }

        public virtual void Stop()
        {
            if (Interlocked.CompareExchange(ref this.startedIf1, 0, 1) == 0)
            {
                return;
            }

            NativeMethods.DeleteTimerQueueTimer(IntPtr.Zero, this.handle, IntPtr.Zero);
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

            if (!this.threadPrioritySet)
            {
                this.changeThreadPriority();
            }
            
            new Thread(() => this.Elapsed?.Invoke()).Start();
        }

        private void changeThreadPriority()
        {
            Thread.CurrentThread.Priority = this.Priority;
            this.threadPrioritySet = true;
        }

        private bool threadPrioritySet;
        private int startedIf1;
        private IntPtr handle;
        private volatile bool autoReset;
        private ThreadPriority priority;
        private readonly TimerCallback callback;
    }
}
