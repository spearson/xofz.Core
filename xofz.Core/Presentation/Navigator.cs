﻿namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    public class Navigator
    {
        public Navigator()
        {
            this.presenters = new List<Presenter>();
        }

        public virtual void RegisterPresenter(Presenter presenter)
        {
            this.presenters.Add(presenter);
        }

        public virtual void Present<T>() where T : Presenter
        {
            var presenter = this.presenters.FirstOrDefault(p => p is T);
            if (presenter == null)
            {
                return;
            }

            new Thread(() =>
            {
                foreach (var p in this.presenters)
                {
                    p.Stop();
                }

                presenter.Start();
            }).Start();

        }

        public virtual void Present<T>(string name) where T : NamedPresenter
        {
            var matchingPresenters = this.presenters.Where(p => p is T).Cast<T>();
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                new Thread(() =>
                {
                    foreach (var p in this.presenters)
                    {
                        p.Stop();
                    }

                    presenter.Start();
                }).Start();
                
                break;
            }
        }

        public virtual void PresentFluidly<T>() where T : Presenter
        {
            var presenter = this.presenters.FirstOrDefault(p => p is T);
            if (presenter == null)
            {
                return;
            }

            new Thread(() => presenter.Start()).Start();
        }

        public virtual void PresentFluidly<T>(string name) where T : NamedPresenter
        {
            var matchingPresenters = this.presenters.Where(p => p is T).Cast<T>();
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                new Thread(() => presenter.Start()).Start();
                break;
            }
        }

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = "ui")
            where TPresenter : Presenter
        {
            var matchingPresenters = this.presenters.Where(p => p is TPresenter);
            if (presenterName == null)
            {
                return this.getUi<TUi>(
                    matchingPresenters.First(),
                    fieldName);
            }

            foreach (var p in matchingPresenters)
            {
                if (!(p is NamedPresenter))
                {
                    continue;
                }

                var np = (NamedPresenter)p;
                if (np.Name == presenterName)
                {
                    return this.getUi<TUi>(
                        np,
                        fieldName);
                }
            }

            return default(TUi);
        }

        private TUi getUi<TUi>(object presenter, string fieldName)
        {
            return (TUi)presenter
                .GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(presenter);
        }

        private readonly List<Presenter> presenters;
    }
}
