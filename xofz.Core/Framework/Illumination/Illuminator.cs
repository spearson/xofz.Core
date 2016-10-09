    namespace xofz.Framework.Illumination
    {
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Reflection;
        using Materialization;

        public class Illuminator
        {
            public virtual T Illumine<T>(params object[] dependencies)
                where T : class
            {
                return this.Illumine<T>(
                    new ArrayMaterializedEnumerable<object>(dependencies));
            }

            public virtual T Illumine<T>(MaterializedEnumerable<object> dependencies)
                where T : class
            {
                var constructors = new LinkedList<ConstructorInfo>(
                    typeof(T).GetConstructors().OrderByDescending(ci => ci.GetParameters().Length));
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
