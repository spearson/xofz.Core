namespace xofz.Misc.Framework.Erudition
{
    using System;
    using System.Collections.Generic;

    public class Slicer
    {
        public virtual Tuple<LiberatedObject, LiberatedObject> Slice(
            LiberatedObject anObject, 
            int slicePoint)
        {
            if (anObject == null)
            {
                return Tuple.Create<LiberatedObject, LiberatedObject>(
                    null,
                    null);
            }

            var dc = anObject.DependencyCount;
            if (slicePoint > dc)
            {
                return Tuple.Create<LiberatedObject, LiberatedObject>(
                    null,
                    null);
            }

            var oneDeps = new LinkedList<object>();
            for (var i = 0; i < slicePoint; ++i)
            {
                oneDeps.AddLast(anObject[i]);
            }

            var twoDeps = new LinkedList<object>();
            for (var i = slicePoint; i < dc; ++i)
            {
                twoDeps.AddLast(anObject[i]);
            }

            return Tuple.Create(
                new LiberatedObject(oneDeps),
                new LiberatedObject(twoDeps));
        }
    }
}
