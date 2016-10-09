﻿namespace xofz.Framework.AI
{
    public class MutableHashCode
    {
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return this.hashCode;
        }

        public virtual void SetHashCode(int hashCode)
        {
            this.hashCode = hashCode;
        }

        private int hashCode;
    }
}
