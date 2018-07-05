namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using xofz.Framework.Materialization;

    public class IniReaderWriter
    {
        public IniReaderWriter(string filePath)
            : this(
                () => File.Exists(filePath)
                    ? File.ReadAllLines(filePath)
                    : new string[0],
                lines => File.WriteAllLines(
                    filePath,
                    lines))
        {
        }

        public IniReaderWriter(
            Func<string[]> readLines,
            Action<IEnumerable<string>> writeLines)
        {
            this.readLines = readLines;
            this.writeLines = writeLines;
        }
        
        public virtual MaterializedEnumerable<string> ReadSectionNames()
        {
            return new LinkedListMaterializedEnumerable<string>(
                this.readSectionHeaders(this.readLines())
                    .Select(header => header.Name));
        }

        public virtual MaterializedEnumerable<string> ReadKeysInSection(
            string sectionName)
        {
            var lines = this.readLines();
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = headers.FirstOrDefault(
                h => h.Name == sectionName);
            return this.readKeysInSectionInternal(
                lines,
                targetHeader);
        }

        public virtual string ReadValue(string sectionName, string key)
        {
            var lines = this.readLines();
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = headers.FirstOrDefault(
                h => h.Name == sectionName);
            if (targetHeader == default(SectionHeader))
            {
                return null;
            }

            if (!this.readKeysInSectionInternal(lines, targetHeader)
                .Contains(key))
            {
                return null;
            }

            var targetLine = default(string);
            int endOfKey;
            foreach (var line in lines
                .Skip(targetHeader.LineNumber))
            {
                endOfKey = line.IndexOf('=');
                if (line.Substring(0, endOfKey) == key)
                {
                    targetLine = line;
                    break;
                }
            }

            if (targetLine == default(string))
            {
                return null;
            }

            var startIndexOfValue = targetLine.IndexOf('=') + 1;
            int valueLength;
            var indexOfSemicolon = targetLine.IndexOf(';');
            if (indexOfSemicolon > -1)
            {
                valueLength = indexOfSemicolon - startIndexOfValue;
            }
            else
            {
                valueLength = targetLine.Length - startIndexOfValue;
            }

            return targetLine.Substring(startIndexOfValue, valueLength);
        }

        public virtual void ChangeValue(
            string sectionName, 
            string key, 
            string newValue)
        {
            var lines = this.readLines();
            var sectionHeaders = this.readSectionHeaders(lines);
            var targetHeader = default(SectionHeader);
            var nextHeaderCounter = 1;
            foreach (var header in sectionHeaders)
            {
                if (header.Name == sectionName)
                {
                    targetHeader = header;
                    break;
                }

                ++nextHeaderCounter;
            }
            if (targetHeader == default(SectionHeader))
            {
                return;
            }
          
            if (!this.readKeysInSectionInternal(lines, targetHeader)
                .Contains(key))
            {
                return;
            }

            var nextHeader = sectionHeaders.Skip(
                nextHeaderCounter).FirstOrDefault();
            var startOfSection = targetHeader.LineNumber;
            var endOfSection = lines.Length;
            if (nextHeader != default(SectionHeader))
            {
                endOfSection = nextHeader.LineNumber - 1;
            }

            for (var i = startOfSection; i < endOfSection; ++i)
            {
                if (!lines[i].StartsWith(key))
                {
                    continue;
                }

                var sb = new StringBuilder(
                    lines[i]);
                var valueIndex = lines[i].IndexOf('=') + 1;
                var indexOfComment = lines[i].IndexOf(';');
                if (indexOfComment < 0)
                {
                    sb.Replace(
                        lines[i].Substring(
                            valueIndex),
                        newValue);
                }
                else
                {
                    sb.Replace(
                        lines[i].Substring(
                            valueIndex,
                            indexOfComment - valueIndex),
                        newValue);
                }

                lines[i] = sb.ToString();
                this.writeLines(lines);
                return;
            }
        }

        protected virtual MaterializedEnumerable<SectionHeader> readSectionHeaders(
            IEnumerable<string> lines)
        {
            var ll = new LinkedList<SectionHeader>();
            var lineNumber = 0;
            foreach (var line in lines)
            {
                ++lineNumber;
                if (!line.StartsWith("["))
                {
                    continue;
                }

                if (!line.Contains("]"))
                {
                    continue;
                }

                var closingBracketIndex = line.IndexOf(']');
                if (line
                    .Substring(0, closingBracketIndex)
                    .Contains(';'))
                {
                    continue;
                }

                var sectionName = line.Substring(
                    1,
                    closingBracketIndex - 1);
                ll.AddLast(
                    new SectionHeader
                    {
                        Name = sectionName,
                        LineNumber = lineNumber
                    });
            }

            return new LinkedListMaterializedEnumerable<SectionHeader>(
                ll);
        }

        private MaterializedEnumerable<string> readKeysInSectionInternal(
            string[] lines,
            SectionHeader targetHeader)
        {
            if (targetHeader == default(SectionHeader))
            {
                return MaterializedEnumerable.Empty<string>();
            }

            var headers = this.readSectionHeaders(lines);
            if (!headers.Select(h => h.Name).Contains(targetHeader.Name))
            {
                return MaterializedEnumerable.Empty<string>();
            }

            var ll = new LinkedList<string>();
            var lineNumber = targetHeader.LineNumber;// todo: get section index from rsni()
            int lastLineIndex;
            if (headers.Select(h => h.Name).Last() == targetHeader.Name)
            {
                lastLineIndex = lines.Length - 1;
            }
            else
            {
                var headerCounter = 1;
                foreach (var header in headers)
                {
                    if (header.Name == targetHeader.Name)
                    {
                        break;
                    }

                    ++headerCounter;
                }

                var nextHeader = headers.Skip(headerCounter)
                    .FirstOrDefault();
                lastLineIndex = nextHeader?.LineNumber - 1
                    ?? lines.Length - 1;
            }

            for (var i = lineNumber; i <= lastLineIndex; ++i)
            {
                var indexOfSemicolon = lines[i].IndexOf(';');
                if (indexOfSemicolon == 0)
                {
                    continue;
                }

                var indexOfEquals = lines[i].IndexOf('=');
                if (indexOfEquals < 0)
                {
                    continue;
                }

                if (indexOfSemicolon > -1 &&
                    indexOfSemicolon < indexOfEquals)
                {
                    continue;
                }

                var keyName = lines[i]
                    .Substring(0, indexOfEquals);
                ll.AddLast(keyName);
            }

            return new LinkedListMaterializedEnumerable<string>(
                ll);
        }

        private readonly Func<string[]> readLines;
        private readonly Action<IEnumerable<string>> writeLines;

        protected class SectionHeader
        {
            public virtual string Name { get; set; }

            public virtual int LineNumber { get; set; }
        }
    }
}
