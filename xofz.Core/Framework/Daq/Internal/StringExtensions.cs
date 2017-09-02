// ----------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.Collections.Generic;

    internal static class StringExtensions
    {
        public static string JoinedWith(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static IEnumerable<string> InChunksOf(this string s, int chunkSize)
        {
            if (chunkSize < 1)
            {
                throw new ArgumentException("Chunk size must be positive.", "chunkSize");
            }

            var chunks = new List<string>();
            for (var i = 0; i < s.Length; i += chunkSize)
            {
                var remainingLength = s.Length - i;
                var currentChunkSize = Math.Min(chunkSize, remainingLength);
                var currentChunk = s.Substring(i, currentChunkSize);
                chunks.Add(currentChunk);
            }

            return chunks;
        }
    }
}
