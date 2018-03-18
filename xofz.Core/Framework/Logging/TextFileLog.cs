namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using xofz.Framework.Materialization;

    public sealed class TextFileLog : Log, LogEditor
    {
        public TextFileLog(string filePath)
        {
            this.filePath = filePath;
        }
        
        public event Action<LogEntry> EntryWritten;

        IEnumerable<LogEntry> Log.ReadEntries()
        {
            lock (this.locker)
            {
                if (!File.Exists(this.filePath))
                {
                    yield break;
                }

                using (var reader = File.OpenText(this.filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string timestampString;
                        while ((timestampString = reader.ReadLine()) == string.Empty)
                        {
                        }

                        var type = reader.ReadLine();
                        var content = new LinkedList<string>();
                        string contentLine;
                        readContent:
                        while ((contentLine = reader.ReadLine()) != string.Empty
                               && contentLine != null)
                        {
                            content.AddLast(contentLine);
                        }

                        if (contentLine == string.Empty)
                        {
                            if (!string.IsNullOrEmpty(contentLine = reader.ReadLine()))
                            {
                                content.AddLast(string.Empty);
                                content.AddLast(contentLine);
                                goto readContent;
                            }
                        }

                        DateTime timestamp;
                        if (DateTime.TryParseExact(
                            timestampString,
                            this.timestampFormat,
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out timestamp))
                        {
                            yield return new LogEntry(
                                timestamp,
                                type,
                                new LinkedListMaterializedEnumerable<string>(content));
                        }
                    }
                }
            }
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
            LogEditor editor = this;
            editor.AddEntry(
                new LogEntry(
                    type,
                    new LinkedListMaterializedEnumerable<string>(
                        content)));
        }

        void LogEditor.AddEntry(LogEntry entry)
        {
            var lines = new LinkedList<string>();
            lines.AddLast(entry.Timestamp.ToString(this.timestampFormat));
            lines.AddLast(entry.Type);
            foreach (var line in entry.Content)
            {
                lines.AddLast(line);
            }

            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);

            lock (this.locker)
            {
                File.AppendAllLines(this.filePath, lines);
            }

            new Thread(() => this.EntryWritten?.Invoke(entry)).Start();
        }

        void LogEditor.Clear()
        {
            lock (this.locker)
            {
                if (File.Exists(this.filePath))
                {
                    File.Delete(this.filePath);
                }
            }
        }

        void LogEditor.Clear(string backupLocation)
        {
            lock (this.locker)
            {
                if (!File.Exists(this.filePath))
                {
                    return;
                }

                File.Copy(this.filePath, backupLocation);
                File.Delete(this.filePath);
            }
        }

        private readonly string filePath;
        private readonly string timestampFormat = "yyyy MMMM dd hh:mm.ss tt";
        private readonly object locker = new object();
    }
}
