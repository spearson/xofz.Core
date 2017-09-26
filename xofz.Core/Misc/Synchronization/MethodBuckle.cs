namespace xofz.Misc.Synchronization
{
    using System;
    using System.Threading;

    public class MethodBuckle
    {
        public MethodBuckle()
        {
            this.latch = new ManualResetEvent(true);
            this.syncer = new object();
        }

        public virtual ManualResetEvent Latch => this.latch;

        public virtual void Buckle(
            Action first)
        {
            var l = this.latch;
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
            var l = this.latch;
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
            var l = this.latch;
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
            var l = this.latch;
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

        private readonly object syncer;
        private readonly ManualResetEvent latch;
    }
}
