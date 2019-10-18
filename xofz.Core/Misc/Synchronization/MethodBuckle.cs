namespace xofz.Misc.Synchronization
{
    using System;
    using System.Threading;

    public class MethodBuckle
    {
        public MethodBuckle()
        {
            this.manualLatch = new ManualResetEvent(true);
            this.syncer = new object();
        }

        public virtual ManualResetEvent Latch => this.manualLatch;

        public virtual void Buckle(
            Action first)
        {
            var l = this.manualLatch;
            l.Reset();
            lock (this.syncer)
            {
                first();
            }
            l.Set();
        }

        public virtual void Buckle(
            Action first, 
            Action second)
        {
            var l = this.manualLatch;
            l.Reset();
            lock (this.syncer)
            {
                first();
                second();
            }
            l.Set();
        }

        public virtual void Buckle(
            Action first, 
            Action second, 
            Action third)
        {
            var l = this.manualLatch;
            l.Reset();
            lock (this.syncer)
            {
                first();
                second();
                third();
            }
            l.Set();
        }

        public virtual void Buckle(
            Action first,
            Action second,
            Action third,
            Action fourth)
        {
            var l = this.manualLatch;
            l.Reset();
            lock (this.syncer)
            {
                first();
                second();
                third();
                fourth();
            }
            l.Set();
        }

        protected readonly object syncer;
        protected readonly ManualResetEvent manualLatch;
    }
}
