﻿namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using xofz.Framework.Materialization;

    public sealed class EventLogLog
        : Log, LogEditor
    {
        public EventLogLog(
            string logName,
            string sourceName)
        {
            this.eventLog = new EventLog(logName)
            {
                Source = sourceName
            };
        }

        public event Action<LogEntry> EntryWritten;

        public event Action Cleared;

        IEnumerable<LogEntry> Log.ReadEntries()
        {
            foreach (EventLogEntry entry in this.eventLog.Entries)
            {
                yield return new LogEntry(
                    entry.TimeWritten,
                    getEntryType(entry),
                    new LinkedListMaterializedEnumerable<string>(
                        new[]
                        {
                            entry.Message
                        }));
            }
        }

        private static string getEntryType(EventLogEntry entry)
        {
            switch (entry.EntryType)
            {
                case EventLogEntryType.Information:
                    return "Information";
                case EventLogEntryType.Error:
                    return "Error";
                case EventLogEntryType.Warning:
                    return "Warning";
                case EventLogEntryType.SuccessAudit:
                    return "Successful Audit";
                case EventLogEntryType.FailureAudit:
                    return "Audit failure";
            }

            return "Information";
        }

        MaterializedEnumerable<LogEntry> Log.ReadEntries(
            DateTime oldestTimestamp)
        {
            Log log = this;
            var ll = new LinkedList<LogEntry>();
            foreach (var entry in log
                .ReadEntries()
                .OrderByDescending(e => e.Timestamp))
            {
                if (entry.Timestamp < oldestTimestamp)
                {
                    break;
                }

                ll.AddLast(entry);
            }

            return new LinkedListMaterializedEnumerable<LogEntry>(ll);
        }

        void LogEditor.AddEntry(string type, IEnumerable<string> content)
        {
            var entry = new LogEntry(
                type,
                new LinkedListMaterializedEnumerable<string>(
                    content));
            LogEditor editor = this;
            editor.AddEntry(entry);
        }

        void LogEditor.AddEntry(LogEntry entry)
        {
            this.eventLog.WriteEntry(
                string.Join(
                    Environment.NewLine,
                    entry.Content),
                getEventLogEntryType(entry.Type));
            this.EntryWritten?.Invoke(entry);
        }

        private static EventLogEntryType getEventLogEntryType(string type)
        {
            switch (type)
            {
                case "Warning":
                    return EventLogEntryType.Warning;
                case "Error":
                    return EventLogEntryType.Error;
                case "Audit Failure":
                    return EventLogEntryType.FailureAudit;
                case "Successful Audit":
                    return EventLogEntryType.SuccessAudit;
            }

            return EventLogEntryType.Information;
        }


        void LogEditor.Clear()
        {
            this.eventLog.Clear();
            this.Cleared?.Invoke();
        }

        void LogEditor.Clear(string backupLocation)
        {
            LogEditor newLog = new TextFileLog(backupLocation);
            var oldLog = this.eventLog;

            foreach (EventLogEntry entry in oldLog.Entries)
            {
                newLog.AddEntry(
                    new LogEntry(
                        entry.TimeWritten,
                        getEntryType(entry),
                        new LinkedListMaterializedEnumerable<string>(
                            new[]
                            {
                                entry.Message
                            })
                    ));
            }

            oldLog.Clear();
            this.Cleared?.Invoke();
        }

        private readonly EventLog eventLog;
    }
}
