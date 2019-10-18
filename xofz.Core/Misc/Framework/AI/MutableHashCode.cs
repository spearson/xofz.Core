namespace xofz.Misc.Framework.AI
{
    public class MutableHashCode
    {
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return this.hashCode;
        }

        public virtual void SetHashCode(
            int hashCode)
        {
            this.hashCode = hashCode;
        }

        protected int hashCode;
    }
}
