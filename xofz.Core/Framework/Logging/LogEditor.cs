﻿namespace xofz.Framework.Logging
{
    using System.Collections.Generic;

    public interface LogEditor
    {
        void AddEntry(string type, IEnumerable<string> content);

        void AddEntry(LogEntry entry);

        void Clear();

        void Clear(string backupLocation);
    }
}
