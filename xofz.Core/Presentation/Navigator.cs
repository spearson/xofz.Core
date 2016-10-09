namespace xofz.Presentation
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

        public virtual TUi GetUi<TPresenter, TUi>(string name = null) where TPresenter : Presenter
        {
            var matchingPresenters = this.presenters.Where(p => p is TPresenter);
            if (name == null)
            {
                return this.getUi<TUi>(matchingPresenters.First());
            }

            foreach (var p in matchingPresenters)
            {
                if (!(p is NamedPresenter))
                {
                    continue;
                }

                var np = (NamedPresenter)p;
                if (np.Name == name)
                {
                    return this.getUi<TUi>(np);
                }
            }

            return default(TUi);
        }

        private TUi getUi<TUi>(object presenter)
        {
            return (TUi)presenter.GetType().GetField("ui", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(presenter);
        }

        private readonly List<Presenter> presenters;
    }
}
