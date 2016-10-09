namespace xofz.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using Materialization;

    public sealed class TextFileLog : Log, LogEditor
    {
        public TextFileLog(string filePath)
        {
            this.filePath = filePath;
        }
        
        public event Action<LogEntry> EntryWritten;

        public IEnumerable<LogEntry> ReadEntries()
        {
            using (var reader = File.OpenText(this.filePath))
            {
                while (!reader.EndOfStream)
                {
                    string timestampString;
                    while((timestampString = reader.ReadLine()) == string.Empty)
                    {
                    }

                    var type = reader.ReadLine();
                    var content = new LinkedList<string>();
                    string contentLine;
                    while((contentLine = reader.ReadLine()) != string.Empty)
                    {
                        content.AddLast(contentLine);
                    }

                    var timestamp = DateTime.Parse(
                        timestampString, 
                        CultureInfo.CurrentCulture);

                    yield return new LogEntry(
                        timestamp, 
                        type,
                        new LinkedListMaterializedEnumerable<string>(content));

                }
            }
        }

        public MaterializedEnumerable<LogEntry> ReadEntries(DateTime oldestTimestamp)
        {
            var ll = new LinkedList<LogEntry>();
            foreach (var entry in this.ReadEntries())
            {
                if (entry.Timestamp < oldestTimestamp)
                {
                    break;
                }

                ll.AddLast(entry);
            }

            return new LinkedListMaterializedEnumerable<LogEntry>(ll);
        }

        public void AddEntry(string type, IEnumerable<string> content)
        {
            this.AddEntry(
                new LogEntry(
                    type,
                    new LinkedListMaterializedEnumerable<string>(
                        content)));
        }

        public void AddEntry(LogEntry entry)
        {
            var lines = new LinkedList<string>();
            lines.AddLast(entry.Timestamp.ToString("yyyy MMMM dd hh:mm.ss"));
            lines.AddLast(entry.Type);
            foreach (var line in entry.Content)
            {
                lines.AddLast(line);
            }

            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);

            File.AppendAllLines(this.filePath, lines);
            new Thread(() => this.EntryWritten?.Invoke(entry)).Start();
        }

        private readonly string filePath;
    }
}
