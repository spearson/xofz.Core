// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="StringData.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace xofz.Framework.Plc
{
    using System;

    public sealed class StringData
    {
        public StringData(int maxLength, int currentLength, string data)
        {
            if (data.Length > 0 && data[data.Length - 1] == '\0')
            {
                data = data.Substring(0, data.Length - 1);
            }

            this.MaxLength = maxLength;
            if (currentLength != data.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(currentLength),
                    @"Current length does not match data length");
            }

            this.Data = data;
        }

        public int MaxLength { get; }

        public string Data { get; }

        public override string ToString()
        {
            return this.Data;
        }
    }
}
