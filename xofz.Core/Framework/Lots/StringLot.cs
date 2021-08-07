namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class StringLot
        : GetArray<char>
    {
        public StringLot(
            string s)
        {
            if (s == null)
            {
                s = string.Empty;
            }

            this.s = s;
        }

        public char this[long index] =>
            this.s[(int)index];

        public virtual IEnumerator<char> GetEnumerator()
        {
            return this.s.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual string Value => this.s;

        public virtual long Count => this.s.Length;

        public virtual void CopyTo(
            char[] array)
        {
            const byte zero = 0;
            this.s.CopyTo(
                zero, 
                array, 
                zero, 
                (int)this.Count);
        }

        public virtual bool Contains(
            char item)
        {
            const byte one = 1;
            return this.s.Contains(
                new string(
                    item, 
                    one));
        }

        protected readonly string s;
    }
}
