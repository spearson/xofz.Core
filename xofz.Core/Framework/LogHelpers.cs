﻿namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LogHelpers
    {
        public static void AddEntry(LogEditor logEditor, Exception e)
        {
            var content = new List<string>
                              {
                                  e.GetType().ToString(),
                                  e.Message,
                                  string.Empty,
                                  "Stack trace:",
                              };
            content.AddRange(trimmedStackTraceFor(e));

            if (e.InnerException != null)
            {
                content.Add(string.Empty);
                content.Add(string.Empty);
                content.Add("Inner exception: " + e.InnerException.GetType());
                content.Add(e.InnerException.Message);
                content.Add(string.Empty);
                content.Add("Stack trace:");
                content.AddRange(trimmedStackTraceFor(e.InnerException));
            }

            logEditor.AddEntry("Error: Exception Occurred!", content.ToArray());
        }

        private static IEnumerable<string> trimmedStackTraceFor(Exception e)
        {
            var untrimmedLines = e.StackTrace.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            return untrimmedLines.Select(line => line.Trim());
        }
    }
}
