namespace xofz.Framework.Plc
{
    using System;
    using System.Linq;

    public class StringRegister
    {
        public StringRegister(
            string value,
            int maxLength)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            var l = value.Length;
            if (l > maxLength)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    @"Value length exceeds max length.");
            }

            if (l > 0 && value.Last() == '\0')
            {
                value = value.Substring(0, l - 1);
            }

            this.Value = value;
            this.MaxLength = maxLength;
        }

        public virtual int MaxLength { get; }

        public virtual string Value { get; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
