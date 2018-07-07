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
            this.uiHolders = new LinkedList<UiHolder>();
        }

        public virtual TUi ReadUi<TUi, TPresenter>(
            string presenterName = null,
            string uiName = null)
            where TUi : Ui
        {
            var mz = this.materializer;
            var matches = mz.Materialize(
                this.uiHolders.Where(ui => ui.Content is TUi));
            if (matches.Count == 0)
            {
                return default(TUi);
            }

            if (presenterName == null)
            {
                var match = matches.FirstOrDefault(
                    ui => ui.ContentName == uiName);
                if (match == null)
                {
                    return default(TUi);
                }

                return (TUi)match.Content;
            }

            var namedMatches = mz.Materialize(matches.Where(
                t => t.Presenter is NamedPresenter &&
                     ((NamedPresenter)t.Presenter).Name == presenterName));
            if (namedMatches.Count == 0)
            {
                return default(TUi);
            }

            var uiMatch = namedMatches.FirstOrDefault(
                nm => nm.ContentName == uiName);
            if (uiMatch == null)
            {
                return default(TUi);
            }

            return (TUi)uiMatch.Content;
        }

        public virtual TResult Read<TUi, TResult>(
            object presenter,
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
            var match = this.uiHolders.FirstOrDefault(
                ui => ReferenceEquals(ui.Presenter, presenter)
                         && ui.Content is TUi
                         && ui.ContentName == uiName);
            var matchAsUi = (TUi)match?.Content;
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
            this.uiHolders.AddLast(
                new UiHolder
                {
                    Content = ui,
                    Presenter = presenter,
                    ContentName = uiName
                });
        }

        private readonly Materializer materializer;
        private readonly LinkedList<UiHolder> uiHolders;

        private class UiHolder
        {
            public virtual Ui Content { get; set; }

            public virtual string ContentName { get; set; }

            public virtual object Presenter { get; set; }
        }
    }
}
