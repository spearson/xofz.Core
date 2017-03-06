namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.UI;

    public class CompositeUi
    {
        public CompositeUi()
        {
            this.uis = new List<Tuple<Ui, object>>(8192);
        }

        public virtual TUi ReadUi<TUi, TPresenter>(string name = null)
            where TUi : Ui
        {
            var matches = this.uis.Where(ui => ui.Item1 is TUi).ToList();
            if (matches.Count == 0)
            {
                return default(TUi);
            }

            if (name == null)
            {
                return (TUi)matches.First().Item1;
            }

            var namedMatch = matches.FirstOrDefault(
                t => t.Item2 is NamedPresenter &&
                     ((NamedPresenter)t.Item2).Name == name);
            if (namedMatch == null)
            {
                return default(TUi);
            }

            return (TUi)namedMatch.Item1;
        }

        public virtual TResult Read<TUi, TResult>(
            object presenter,
            Func<TUi, TResult> read)
            where TUi : Ui
        {
            var match = this.uis.FirstOrDefault(
                tuple => tuple.Item2 == presenter && tuple.Item1 is TUi);
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
            Action<TUi> write) 
            where TUi : Ui
        {
            var match = this.uis.FirstOrDefault(
                tuple => tuple.Item2 == presenter && tuple.Item1 is TUi);
            var matchAsUi = (TUi)match?.Item1;
            if (matchAsUi != null)
            {
                UiHelpers.Write(matchAsUi, () => write(matchAsUi));
                return matchAsUi;
            }

            return default(TUi);
        }

        public virtual void Write<TUi>(
            TUi ui,
            Action<TUi> write)
            where TUi : Ui
        {
            UiHelpers.Write(ui, () => write(ui));
        }

        public virtual void Register(Ui ui, object presenter)
        {
            this.uis.Add(Tuple.Create(ui, presenter));
        }

        private readonly IList<Tuple<Ui, object>> uis;
    }
}
