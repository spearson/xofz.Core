namespace xofz.Misc.Framework.Martyr
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using xofz.Framework.Materialization;
    using xofz.Misc.Framework.Illumination;

    public class FreedomHolder
    {
        public FreedomHolder(Martyr martyr, Illuminator illuminator, string name)
        {
            this.martyr = martyr;
            this.illuminator = illuminator;
            this.Name = name;
        }

        public virtual string Name { get; }

        public virtual void Surge(MaterializedEnumerable<IDisposable> disposables)
        {
            var dependencies = new LinkedList<List<object>>();
            foreach (var disposable in disposables)
            {
                dependencies.AddLast(new List<object>());
                var dd = disposable.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                var dToSurge = new List<IDisposable>();
                foreach (var field in dd)
                {
                    var value = field.GetValue(disposable);
                    dependencies.Last.Value.Add(value);
                    if (field.FieldType.Equals(typeof(IDisposable)))
                    {
                        dToSurge.Add((IDisposable)value);
                    }
                }

                this.Surge(new ListMaterializedEnumerable<IDisposable>(dToSurge));

                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                this.martyr.BringToGod(disposables);
                var ll = this.illuminator.Illumine<
                    LinkedListMaterializedEnumerable<IDisposable>>(dependencies);
                this.illuminator.Illumine<IDisposable>(ll);
            }
        }

        private readonly Martyr martyr;
        private readonly Illuminator illuminator;
    }
}
