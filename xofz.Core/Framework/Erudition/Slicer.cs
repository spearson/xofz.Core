namespace xofz.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Slicer
    {
        public virtual Tuple<LiberatedObject, LiberatedObject> Slice(LiberatedObject anObject, int slicePoint)
        {
            if (slicePoint > anObject.DependencyCount)
            {
                return default(Tuple<LiberatedObject, LiberatedObject>);
            }

            var oneDeps = new LinkedList<object>();
            for (var i = 0; i < slicePoint; ++i)
            {
                oneDeps.AddLast(anObject[i]);
            }

            var twoDeps = new LinkedList<object>();
            for (var i = slicePoint; i < anObject.DependencyCount; ++i)
            {
                twoDeps.AddLast(anObject[i]);
            }

            return Tuple.Create(
                new LiberatedObject(oneDeps),
                new LiberatedObject(twoDeps));
        }
    }
}
