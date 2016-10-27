namespace xofz
{
    using System;

    public static class StringHelpers
    {
        public static T ToEnum<T>(string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        public static string RemoveFromEnd(string s, int numberToRemove)
        {
            if (s == null)
            {
                return null;
            }

            return numberToRemove >= s.Length
                ? string.Empty
                : s.Substring(0, s.Length - numberToRemove);
        }
    }
}
