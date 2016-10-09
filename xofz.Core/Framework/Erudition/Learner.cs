namespace xofz.Framework.Erudition
{
    using System;

    public class Learner<T>
    {
        public virtual T Request => this.selectedItem;

        public virtual void Learn(Func<T> instantiator, Action<T> learningMethod)
        {
            var one = instantiator();
            var hashCode = one.GetHashCode();
            learningMethod(one);
            if (hashCode == one.GetHashCode())
            {
                this.setSelectedItem(one);
                return;
            }

            this.setSelectedItem(instantiator());
        }

        private void setSelectedItem(T selectedItem)
        {
            this.selectedItem = selectedItem;
        }

        private T selectedItem;
    }
}
