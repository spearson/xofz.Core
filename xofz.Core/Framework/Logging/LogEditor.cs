namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;

    public interface LogEditor
    {
        event Action Cleared;

        void AddEntry(string type, IEnumerable<string> content);

        void AddEntry(LogEntry entry);

        void Clear();

        void Clear(string backupLocation);
    }
}
