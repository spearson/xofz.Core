namespace xofz.Framework
{
    using System;
    using System.Linq;

    public class LogStatistics
    {
        public LogStatistics(MethodWeb web)
        {
            this.web = web;
        }

        public virtual string LogName { get; set; }

        public virtual long TotalEntryCount { get; set; }

        public virtual double AvgEntriesPerDay { get; protected set; }

        public virtual DateTime OldestTimestamp { get; protected set; }

        public virtual DateTime NewestTimestamp { get; protected set; }

        public virtual DateTime EarliestTimestamp { get; protected set; }

        public virtual DateTime LatestTimestamp { get; protected set; }

        public virtual void ComputeOverall()
        {
            var w = this.web;
            w.Run<Log>(l =>
                {
                    var allEntries = w.Run<Materializer,
                        MaterializedEnumerable<LogEntry>>(
                        m => m.Materialize(l.ReadEntries()),
                        "LogMaterializer");
                    var start = DateTime.MaxValue;
                    var end = DateTime.MinValue;
                    foreach (var entry in allEntries)
                    {
                        if (entry.Timestamp < start)
                        {
                            start = entry.Timestamp;
                        }

                        if (entry.Timestamp > end)
                        {
                            end = entry.Timestamp;
                        }
                    }

                    this.computeTotal(allEntries);
                    this.computeAvgPerDay(
                        allEntries.Count,
                        start,
                        end);
                    this.computeOldestTimestamp(allEntries);
                    this.computeNewestTimestamp(allEntries);
                    this.computeEarliestTimestamp(allEntries);
                    this.computeLatestTimestamp(allEntries);
                },
                this.LogName);
        }

        public virtual void ComputeRange(
            DateTime startDate,
            DateTime endDate)
        {
            var w = this.web;
            w.Run<Log>(l =>
                {
                    var entries = w.Run<Materializer,
                        MaterializedEnumerable<LogEntry>>(
                        m => m.Materialize(l.ReadEntries()
                            .Where(
                                e => e.Timestamp >= startDate
                                     && e.Timestamp < endDate.AddDays(1))),
                        "LogMaterializer");
                    this.computeTotal(entries);
                    this.computeAvgPerDay(
                        entries.Count,
                        startDate,
                        endDate);
                    this.computeOldestTimestamp(entries);
                    this.computeNewestTimestamp(entries);
                    this.computeEarliestTimestamp(entries);
                    this.computeLatestTimestamp(entries);
                },
                this.LogName);
        }

        public virtual void Reset()
        {
            var ts = default(DateTime);
            this.OldestTimestamp = ts;
            this.NewestTimestamp = ts;
            this.EarliestTimestamp = ts;
            this.LatestTimestamp = ts;
            this.AvgEntriesPerDay = default(double);
            this.TotalEntryCount = 0;
        }

        private void computeTotal(MaterializedEnumerable<LogEntry> entries)
        {
            this.TotalEntryCount = entries.Count;
        }

        private void computeAvgPerDay(
            long entryCount,
            DateTime start,
            DateTime end)
        {
            if (entryCount == 0)
            {
                this.AvgEntriesPerDay = default(double);
                return;
            }

            var totalDays = (end - start).TotalDays + 1;
            this.AvgEntriesPerDay = entryCount / totalDays;
        }

        private void computeOldestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.OldestTimestamp = default(DateTime);
                return;
            }

            var oldest = DateTime.MaxValue;
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts < oldest)
                {
                    oldest = ts;
                }
            }

            this.OldestTimestamp = oldest;
        }

        private void computeNewestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.NewestTimestamp = default(DateTime);
                return;
            }

            var newest = DateTime.MinValue;
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts > newest)
                {
                    newest = ts;
                }
            }

            this.NewestTimestamp = newest;
        }

        private void computeEarliestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.EarliestTimestamp = default(DateTime);
                return;
            }

            var earliest = new DateTime(1, 1, 1, 23, 59, 59);
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts.TimeOfDay < earliest.TimeOfDay)
                {
                    earliest = ts;
                }
            }

            this.EarliestTimestamp = earliest;
        }

        private void computeLatestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.LatestTimestamp = default(DateTime);
                return;
            }

            var latest = new DateTime(1, 1, 1, 0, 0, 0);
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts.TimeOfDay > latest.TimeOfDay)
                {
                    latest = ts;
                }
            }

            this.LatestTimestamp = latest;
        }

        private readonly MethodWeb web;
    }
}
