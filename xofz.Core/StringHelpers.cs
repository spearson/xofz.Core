namespace xofz
{
    using System;
    using System.Collections.Generic;

    public static class StringHelpers
    {
        public static T ToEnum<T>(string s)
            where T : struct
        {
            return Enum.TryParse(s, out T e) ? e : default(T);
        }

        public static string RemoveEndChars(string s, int count)
        {
            if (s == null)
            {
                return null;
            }

            return count >= s.Length
                ? string.Empty
                : s.Substring(
                    0, 
                    s.Length - count);
        }

        public static IEnumerable<string> Chunks(string s, int chunkSize)
        {
            if (s == null)
            {
                yield break;
            }

            if (chunkSize < 1)
            {
                yield return s;
            }

            var l = s.Length;
            int i;
            for (i = 0; i < l - chunkSize; i += chunkSize)
            {
                yield return s.Substring(i, chunkSize);
            }

            if (i < l)
            {
                yield return s.Substring(i, l - i);
            }
        }
    }
}
