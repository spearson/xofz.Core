namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class StringLot
        : Lot<char>
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

        public virtual IEnumerator<char> GetEnumerator()
        {
            return this.s.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual long Count => this.s.Length;

        public virtual void CopyTo(
            char[] array)
        {
            this.s.CopyTo(
                0, 
                array, 
                0, 
                (int)this.Count);
        }

        public virtual bool Contains(
            char item)
        {
            return this.s.Contains(
                new string(
                    item, 
                    1));
        }

        protected readonly string s;
    }
}
