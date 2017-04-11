﻿namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableSplitter
    {
        public virtual MaterializedEnumerable<T>[] Split<T>(IEnumerable<T> source, int splits)
        {
            var array = new LinkedListMaterializedEnumerable<T>[splits];
            for (var i = 0; i < splits; ++i)
            {
                array[i] = new LinkedListMaterializedEnumerable<T>();
            }

            var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            array[0].AddLast(enumerator.Current);
            var zeroFilled = true;
            while (enumerator.MoveNext())
            {
                for (var i = 0; i < splits; ++i)
                {
                    if (zeroFilled && i == 0)
                    {
                        continue;
                    }

                    zeroFilled = false;
                    array[i].AddLast(enumerator.Current);
                    enumerator.MoveNext();
                }

                zeroFilled = false;
            }

            var generalArray = new MaterializedEnumerable<T>[splits];
            for (var i = 0; i < splits; ++i)
            {
                generalArray[i] = array[i];
            }

            enumerator.Dispose();
            return generalArray;
        }
    }
}
