namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework.Materialization;
    using xofz.UI;

    public class CompositeUi
    {
        public CompositeUi()
            : this(new LinkedListMaterializer())
        {
        }

        public CompositeUi(Materializer materializer)
        {
            this.materializer = materializer;
            this.uis = new List<Tuple<Ui, object, string>>(0x10000);
        }

        public virtual TUi ReadUi<TUi, TPresenter>(
            string presenterName = null,
            string uiName = null)
            where TUi : Ui
        {
            var matches = this.materializer.Materialize(
                this.uis.Where(ui => ui.Item1 is TUi));
            if (matches.Count == 0)
            {
                return default(TUi);
            }

            if (presenterName == null)
            {
                var match = matches.FirstOrDefault(
                    tuple => tuple.Item3 == uiName);
                if (match == null)
                {
                    return default(TUi);
                }

                return (TUi)match.Item1;
            }

            var namedMatches = matches.Where(
                t => t.Item2 is NamedPresenter &&
                     ((NamedPresenter)t.Item2).Name == presenterName)
                     .ToList();
            if (namedMatches.Count == 0)
            {
                return default(TUi);
            }

            var uiMatch = namedMatches.FirstOrDefault(
                nm => nm.Item3 == uiName);
            if (uiMatch == null)
            {
                return default(TUi);
            }

            return (TUi)uiMatch.Item1;
        }

        public virtual TResult Read<TUi, TResult>(
            object presenter,
            Func<TUi, TResult> read,
            string uiName = null)
            where TUi : Ui
        {
            var match = this.uis.FirstOrDefault(
                tuple => ReferenceEquals(presenter, tuple.Item2)
                && tuple.Item1 is TUi
                && tuple.Item3 == uiName);
            var matchAsUi = (TUi)match?.Item1;
            return matchAsUi != null 
                ? UiHelpers.Read(matchAsUi, () => read(matchAsUi)) 
                : default(TResult);
        }

        public virtual TResult Read<TUi, TResult>(
            TUi ui,
            Func<TUi, TResult> read)
            where TUi : Ui
        {
            return UiHelpers.Read(ui, () => read(ui));
        }

        public virtual TUi Write<TUi>(
            object presenter, 
            Action<TUi> write,
            string uiName = null) 
            where TUi : Ui
        {
            var match = this.uis.FirstOrDefault(
                tuple => tuple.Item2 == presenter 
                && tuple.Item1 is TUi
                && tuple.Item3 == uiName);
            var matchAsUi = (TUi)match?.Item1;
            if (matchAsUi == null)
            {
                return default(TUi);
            }

            UiHelpers.Write(matchAsUi, () => write(matchAsUi));
            return matchAsUi;
        }

        public virtual void Write<TUi>(
            TUi ui,
            Action<TUi> write)
            where TUi : Ui
        {
            UiHelpers.Write(ui, () => write(ui));
        }

        public virtual void Register(
            Ui ui, 
            object presenter,
            string uiName = null)
        {
            this.uis.Add(
                Tuple.Create(
                    ui, 
                    presenter,
                    uiName));
        }

        private readonly Materializer materializer;
        private readonly IList<Tuple<Ui, object, string>> uis;
    }
}
