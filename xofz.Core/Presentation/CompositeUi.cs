namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.UI;

    public class CompositeUi
    {
        public CompositeUi()
            : this(new LinkedList<UiHolder>())
        {
        }

        protected CompositeUi(
            ICollection<UiHolder> uiHolders)
        {
            this.uiHolders = uiHolders 
                             ?? new LinkedList<UiHolder>();
        }

        public virtual TUi ReadUi<TUi, TPresenter>(
            string uiName = null,
            string presenterName = null)
            where TUi : Ui
            where TPresenter : Presenter
        {
            ICollection<UiHolder> matches = new LinkedList<UiHolder>(
                this.uiHolders.Where(ui => ui.Content is TUi));
            if (matches.Count < 1)
            {
                return default;
            }

            if (presenterName == null)
            {
                var match = matches.FirstOrDefault(
                    ui => ui.ContentName == uiName);
                if (match == null)
                {
                    return default;
                }

                return (TUi)match.Content;
            }

            ICollection<UiHolder> namedMatches = new LinkedList<UiHolder>(
                matches.Where(t =>
                    t.Presenter is NamedPresenter presenter &&
                    presenter.Name == presenterName));
            if (namedMatches.Count < 1)
            {
                return default;
            }

            var uiMatch = namedMatches.FirstOrDefault(
                nm => nm.ContentName == uiName);
            if (uiMatch == null)
            {
                return default;
            }

            return (TUi)uiMatch.Content;
        }

        public virtual TResult Read<TUi, TResult>(
            Presenter presenter,
            Func<TUi, TResult> read,
            string uiName = null)
            where TUi : Ui
        {
            var match = this.uiHolders.FirstOrDefault(
                tuple => ReferenceEquals(presenter, tuple.Presenter)
                && tuple.Content is TUi
                && tuple.ContentName == uiName);
            var matchAsUi = (TUi)match?.Content;
            return matchAsUi != null 
                ? UiHelpers.Read(matchAsUi, () => read(matchAsUi)) 
                : default(TResult);
        }

        public virtual TResult Read<TUi, TResult>(
            TUi ui,
            Func<TResult> read)
            where TUi : Ui
        {
            return UiHelpers.Read(
                ui, 
                () => read());
        }

        public virtual TUi Write<TUi>(
            Presenter presenter, 
            Action<TUi> write,
            string uiName = null) 
            where TUi : Ui
        {
            var match = this.uiHolders.FirstOrDefault(
                ui => ReferenceEquals(ui.Presenter, presenter)
                         && ui.Content is TUi
                         && ui.ContentName == uiName);
            var matchAsUi = (TUi)match?.Content;
            if (matchAsUi == null)
            {
                return default(TUi);
            }

            UiHelpers.Write(
                matchAsUi, 
                () => write(matchAsUi));
            return matchAsUi;
        }

        public virtual void Write<TUi>(
            TUi ui,
            Do write)
            where TUi : Ui
        {
            UiHelpers.Write(ui, write);
        }

        public virtual void Register(
            Ui ui, 
            Presenter presenter,
            string uiName = null)
        {
            this.uiHolders.Add(
                new UiHolder
                {
                    Content = ui,
                    Presenter = presenter,
                    ContentName = uiName
                });
        }

        protected readonly ICollection<UiHolder> uiHolders;

        protected class UiHolder
        {
            public virtual Ui Content { get; set; }

            public virtual string ContentName { get; set; }

            public virtual Presenter Presenter { get; set; }
        }
    }
}
