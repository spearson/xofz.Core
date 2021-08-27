namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using xofz.UI;
    using static EnumerableHelpers;

    public class CompositeUi
    {
        public CompositeUi()
            : this(new XLinkedList<UiHolder>())
        {
        }

        protected CompositeUi(
            ICollection<UiHolder> uiHolders)
            : this(uiHolders, new object())
        {
        }

        protected CompositeUi(
            object locker)
            : this(
                new XLinkedList<UiHolder>(), 
                locker)
        {
        }

        protected CompositeUi(
            ICollection<UiHolder> uiHolders,
            object locker)
        {
            this.uiHolders = uiHolders
                             ?? new XLinkedList<UiHolder>();
            this.locker = locker
                          ?? new object();
        }

        public virtual TUi ReadUi<TUi, TPresenter>(
            string uiName = null,
            string presenterName = null)
            where TUi : Ui
            where TPresenter : Presenter
        {
            const byte one = 1;
            ICollection<UiHolder> matches;
            lock (this.locker)
            {
                matches = XLinkedList<UiHolder>.Create(
                    Where(
                        this.uiHolders,
                        ui => ui.Content is TUi));
            }

            if (matches.Count < one)
            {
                return default;
            }

            if (presenterName == null)
            {
                var match = FirstOrDefault(
                    matches,
                    ui => ui.ContentName == uiName);
                if (match == null)
                {
                    return default;
                }

                return (TUi)match.Content;
            }

            ICollection<UiHolder> namedMatches = XLinkedList<UiHolder>.Create(
                Where(
                    matches,
                    t =>
                    t.Presenter is NamedPresenter presenter &&
                    presenter.Name == presenterName));
            if (namedMatches.Count < one)
            {
                return default;
            }

            var uiMatch = FirstOrDefault(
                namedMatches,
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
            UiHolder match;
            lock (this.locker)
            {
                match = FirstOrDefault(
                    this.uiHolders,
                    holder => ReferenceEquals(presenter, holder.Presenter)
                              && holder.Content is TUi
                              && holder.ContentName == uiName);
            }

            TUi matchAsUi;
            try
            {
                matchAsUi = (TUi)match?.Content;
            }
            catch
            {
                matchAsUi = default;
            }

            return matchAsUi != null
                ? UiHelpers.Read(
                    matchAsUi, 
                    () => read(matchAsUi))
                : default;
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
            UiHolder match;

            lock (this.locker)
            {
                match = FirstOrDefault(
                    this.uiHolders,
                    ui => ReferenceEquals(ui.Presenter, presenter)
                          && ui.Content is TUi
                          && ui.ContentName == uiName);
            }

            TUi matchAsUi;
            try
            {
                matchAsUi = (TUi)match?.Content;
            }
            catch
            {
                matchAsUi = default;
            }
            
            if (matchAsUi == null)
            {
                return default;
            }

            UiHelpers.Write(
                matchAsUi,
                () =>
                {
                    write(matchAsUi);
                });
            return matchAsUi;
        }

        public virtual void Write<TUi>(
            TUi ui,
            Do write)
            where TUi : Ui
        {
            UiHelpers.Write(
                ui, 
                write);
        }

        public virtual bool Register(
            Ui ui, 
            Presenter presenter,
            string uiName = null)
        {
            if (ui == null || presenter == null)
            {
                return falsity;
            }

            lock (this.locker)
            {
                this.uiHolders?.Add(
                    new UiHolder
                    {
                        Content = ui,
                        Presenter = presenter,
                        ContentName = uiName
                    });
            }

            return truth;
        }

        public virtual bool Unregister(
            Ui ui,
            Presenter presenter,
            string uiName = null)
        {
            UiHolder match;
            lock (this.locker)
            {
                var uhs = this.uiHolders;
                match = FirstOrDefault(
                    uhs,
                    holder =>
                        holder != null &&
                        ReferenceEquals(holder.Content, ui) &&
                        ReferenceEquals(holder.Presenter, presenter) &&
                        holder.ContentName == uiName);
                if (match != null)
                {
                    return uhs.Remove(match);
                }
            }

            return falsity;
        }

        protected readonly ICollection<UiHolder> uiHolders;
        protected readonly object locker;
        protected const bool 
            truth = true,
            falsity = false;

        protected class UiHolder
        {
            public virtual Ui Content { get; set; }

            public virtual string ContentName { get; set; }

            public virtual Presenter Presenter { get; set; }
        }
    }
}
