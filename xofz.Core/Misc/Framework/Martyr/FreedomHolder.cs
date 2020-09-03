namespace xofz.Misc.Framework.Martyr
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using xofz.Framework.Lots;
    using xofz.Misc.Framework.Illumination;

    public class FreedomHolder
    {
        public FreedomHolder(
            Martyr martyr, 
            Illuminator illuminator, 
            string name)
        {
            this.martyr = martyr;
            this.illuminator = illuminator;
            this.Name = name;
        }

        public virtual string Name { get; }

        public virtual void Surge(
            Lot<IDisposable> disposables)
        {
            if (disposables == null)
            {
                return;
            }

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
                    if (field.FieldType == typeof(IDisposable))
                    {
                        dToSurge.Add((IDisposable)value);
                    }
                }

                this.Surge(new ListLot<IDisposable>(dToSurge));

                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                this.martyr.BringToGod(disposables);
                var ll = this.illuminator.Illumine<
                    LinkedListLot<IDisposable>>(dependencies);
                this.illuminator.Illumine<IDisposable>(ll);
            }
        }

        protected readonly Martyr martyr;
        protected readonly Illuminator illuminator;
    }
}
