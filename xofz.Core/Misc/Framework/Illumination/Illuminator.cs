    namespace xofz.Misc.Framework.Illumination
    {
        using System;
        using System.Collections.Generic;
        using System.Reflection;
        using xofz.Framework.Lots;
        using EH = xofz.EnumerableHelpers;

        public class Illuminator
        {
            public virtual T Illumine<T>(
                params object[] dependencies)
            {
                return this.Illumine<T>(
                    new LinkedListLot<object>(dependencies));
            }

            public virtual T Illumine<T>(
                Lot<object> dependencies)
            {
                var constructors = new LinkedList<ConstructorInfo>(
                    EH.OrderByDescending(
                        typeof(T).GetConstructors(),
                        ci => ci.GetParameters().Length));
                var list = new List<object>(dependencies);
                foreach (var ctor in constructors)
                {
                    var parameters = ctor.GetParameters();
                    var valuesNeeded = new List<object>(list.Count);
                    foreach (var parameter in parameters)
                    {
                        for (var i = 0; i < list.Count; ++i)
                        {
                            if (!parameter.ParameterType.IsInstanceOfType(list[i]))
                            {
                                continue;
                            }

                            valuesNeeded.Add(list[i]);
                            list.Remove(list[i]);
                            break;
                        }
                    }

                    if (valuesNeeded.Count == parameters.Length)
                    {
                        list = valuesNeeded;
                        break;
                    }

                    // if the objects don't line up with this constructor, start over with the next one
                    list = new List<object>(dependencies);
                    valuesNeeded.Clear();
                }

                return (T)Activator.CreateInstance(typeof(T), list.ToArray());
            }
        }
    }
