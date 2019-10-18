namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Judgement<T>
    {
        public Judgement(
            Absolution<T> absolution)
        {
            this.absolution = absolution;
        }

        public virtual bool Judge(
            Lot<T> lot, 
            Action<T> learn)
        {
            var reflection = new Reflection<T>(lot, this.absolution);
            var hashCodes = new LinkedList<int>();
            var isKnowledge = true;
            foreach (var item in reflection.Reflect(learn))
            {
                var hashCode = item.GetHashCode();
                if (hashCodes.Contains(hashCode))
                {
                    isKnowledge = false;
                    break;
                }

                hashCodes.AddLast(item.GetHashCode());
            }

            return isKnowledge;
        }

        protected readonly Absolution<T> absolution;
    }
}
