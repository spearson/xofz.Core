namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Judgement<T>
    {
        public Judgement(Absolution<T> absolution)
        {
            this.absolution = absolution;
        }

        public virtual bool Judge(MaterializedEnumerable<T> collection, Action<T> learn)
        {
            var reflection = new Reflection<T>(collection, this.absolution);
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

        private readonly Absolution<T> absolution;
    }
}
