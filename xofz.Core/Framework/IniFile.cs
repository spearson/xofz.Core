namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    // todo: clean up!
    public class IniFile
    {
        public IniFile(string location)
        {
            this.location = location;
        }

        // note: modifying the returned list will not change the ini file
        public virtual IList<string> Sections()
        {
            return (from line in File.ReadAllLines(this.location)
                    where line.StartsWith("[") && line.Contains("]")
                    select line.Substring(
                        1,
                        line.IndexOf(
                            "]",
                            StringComparison.CurrentCulture) - 1))
                .ToList();
        }

        public virtual IList<string> KeysInSection(string section)
        {
            var lines = File.ReadAllLines(this.location);
            var keys = new List<string>();
            for (var i = 0; i < lines.Length - 1; ++i)
            {
                if (lines[i].StartsWith(";") || !lines[i].Contains("[" + section + "]"))
                {
                    continue;
                }

                for (var j = i + 1; j < lines.Length; ++j)
                {
                    if (string.IsNullOrWhiteSpace(lines[j]))
                    {
                        continue;
                    }

                    if (lines[j].StartsWith(";"))
                    {
                        continue;
                    }

                    if (lines[j].StartsWith("["))
                    {
                        break;
                    }

                    var indexOfEquals = lines[j].IndexOf('=');
                    if (indexOfEquals < 0)
                    {
                        continue;
                    }

                    keys.Add(lines[j].Substring(0, lines[j].IndexOf('=')));
                }

                break;
            }

            return keys;
        }

        public virtual string Value(string section, string key)
        {
            var lines = File.ReadAllLines(this.location);
            for (var i = 0; i < lines.Length - 1; ++i)
            {
                if (lines[i].StartsWith(";") || !lines[i].Contains("[" + section + "]"))
                {
                    continue;
                }

                for (var j = i + 1; j < lines.Length; ++j)
                {
                    if (lines[j].StartsWith("["))
                    {
                        return string.Empty;
                    }

                    var firstEqualsIndex = lines[j].IndexOf('=');
                    if (firstEqualsIndex == -1)
                    {
                        continue;
                    }

                    var lineKey = lines[j].Substring(0, firstEqualsIndex);
                    if (key == lineKey)
                    {
                        var valueIndex = firstEqualsIndex + 1;
                        return lines[j].Substring(valueIndex, lines[j].Length - valueIndex);
                    }
                }
            }

            return string.Empty;
        }

        public virtual void ChangeValue(string section, string key, string newValue)
        {
            var lines = File.ReadAllLines(this.location);
            for (var i = 0; i < lines.Length - 1; ++i)
            {
                if (!lines[i].Contains("[" + section + "]"))
                {
                    continue;
                }

                for (var j = i + 1; j < lines.Length; ++j)
                {
                    if (lines[j].Contains("["))
                    {
                        return;
                    }

                    var keyAndValue = lines[j].Split('=');
                    if (keyAndValue.Length < 2)
                    {
                        continue;
                    }

                    if (keyAndValue[0] == key)
                    {
                        keyAndValue[1] = newValue;
                        lines[j] = string.Join("=", keyAndValue);
                        File.WriteAllLines(this.location, lines);
                    }
                }
            }
        }

        private readonly string location;
    }
}
