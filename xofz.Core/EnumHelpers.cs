namespace xofz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumHelpers
    {
        public static IEnumerable<T> Iterate<T>()
        {
            // thank you JaredPar for this one
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
