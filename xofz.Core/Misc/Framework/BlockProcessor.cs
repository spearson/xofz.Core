namespace xofz.Misc.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class BlockProcessor
    {
        public BlockProcessor()
        {
            var b = new List<Action>[
                Environment.ProcessorCount];
            for (var i = 0; i < b.Length; ++i)
            {
                b[i] = new List<Action>(0x1000);
            }

            this.blocks = b;
        }

        public virtual BlockProcessor Configure()
        {
            Interlocked.CompareExchange(
                ref this.configuringIf1, 
                1, 
                0);
            return this;
        }

        public virtual BlockProcessor AddBlock(
            Action block)
        {
            if (block == null)
            {
                throw new InvalidOperationException(
                    "Null actions are not supported. "
                    + "If this was intended, please pass "
                    + "an empty block, i.e., () => { }.");
            }

            if (Interlocked.Read(ref this.configuringIf1) == 0)
            {
                throw new InvalidOperationException(
                    "The block processor is not currently being configured.");
            }

            var b = this.blocks;
            b[0].Add(block);
            this.setCurrentProcessor(0);
            for (var i = 1; i < b.Length; ++i)
            {
                b[i].Add(null);
            }

            return this;
        }

        public virtual BlockProcessor ParallelWithLast(
            Action parallelBlock)
        {
            if (parallelBlock == null)
            {
                throw new InvalidOperationException(
                    @"Null actions are not supported. "
                    + @"If this was intended, please pass "
                    + @"an empty block, i.e., () => { }.");
            }

            if (Interlocked.Read(ref this.configuringIf1) != 1)
            {
                throw new InvalidOperationException(
                    @"The block processor is not currently being configured.");
            }

            var cp = this.currentProcessor;
            var b = this.blocks;
            var newCP = (byte)(cp + 1);
            if (newCP >= b.Length)
            {
                throw new InvalidOperationException(
                    @"There are not enough processors to parallelize "
                    + @"this action.");
            }

            
            this.setCurrentProcessor(newCP);
            b[newCP].Add(parallelBlock);

            return this;
        }

        public virtual BlockProcessor Finish()
        {
            Interlocked.CompareExchange(
                ref this.configuringIf1, 
                0, 
                1);
            return this;
        }

        public virtual BlockProcessor Reset()
        {
            var b = this.blocks;
            foreach (var l in b)
            {
                l.Clear();
            }

            this.setCurrentProcessor(0);
            return this;
        }

        public virtual void Process()
        {
            if (Interlocked.Read(
                    ref this.configuringIf1) == 1)
            {
                throw new InvalidOperationException(
                    @"The block processor is currently being configured.");
            }

            var b = this.blocks;
            for (var i = 0; i < b[0].Count; ++i)
            {
                b[0][i]();
                var ts = new Thread[b.Length - 1];
                for (var j = 1; j < b.Length; ++j)
                {
                    if (b[j][i] != null)
                    {
                        var closedJ = j;
                        var closedI = i;
                        ts[j - 1] = new Thread(() => b[closedJ][closedI]());
                    }
                }

                foreach (var t in ts.Where(thread => thread != null))
                {
                    t.Start();
                }

                foreach (var t in ts.Where(thread => thread != null))
                {
                    t.Join();
                }
            }
        }

        protected virtual void setBlocks(
            IList<Action>[] blocks)
        {
            this.blocks = blocks;
        }

        protected virtual void setCurrentProcessor(
            byte currentProcessor)
        {
            this.currentProcessor = currentProcessor;
        }

        protected byte currentProcessor;
        protected long configuringIf1;
        protected IList<Action>[] blocks;
    }
}
