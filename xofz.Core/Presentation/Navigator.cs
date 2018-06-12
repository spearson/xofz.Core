﻿namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using xofz.Framework;

    public class Navigator
    {
        public Navigator(MethodWeb web)
        {
            this.web = web;
            this.presenters = new LinkedList<Presenter>();
        }

        public virtual void RegisterPresenter(Presenter presenter)
        {
            this.presenters.AddLast(presenter);
        }

        public virtual bool IsRegistered<T>() 
            where T : Presenter
        {
            return this.presenters
                .OfType<T>()
                .Any();
        }

        public virtual bool IsRegistered<T>(string name) 
            where T : NamedPresenter
        {
            return this.presenters
                .OfType<T>()
                .Any(np => np.Name == name);

        }

        public virtual void Present<T>() where T : Presenter
        {
            var ps = this.presenters;
            var presenter = ps.FirstOrDefault(p => p is T);
            if (presenter == null)
            {
                return;
            }

            foreach (var p in ps)
            {
                p.Stop();
            }

            ThreadPool.QueueUserWorkItem(o => presenter.Start());
        }

        public virtual void Present<T>(string name) 
            where T : NamedPresenter
        {
            var ps = this.presenters;
            var matchingPresenters = ps.Where(p => p is T).Cast<T>();
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                foreach (var p in ps)
                {
                    p.Stop();
                }

                ThreadPool.QueueUserWorkItem(o => presenter.Start());
                break;
            }
        }

        public virtual void PresentFluidly<T>() 
            where T : Presenter
        {
            var presenter = this.presenters.FirstOrDefault(p => p is T);
            if (presenter == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => presenter.Start());
        }

        public virtual void PresentFluidly<T>(string name) 
            where T : NamedPresenter
        {
            var matchingPresenters = this.presenters.Where(p => p is T).Cast<T>();
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                ThreadPool.QueueUserWorkItem(o => presenter.Start());
                break;
            }
        }

        public virtual void LoginFluidly()
        {
            var w = this.web;
            w.Run<LatchHolder>(
                latch => latch.Latch.Reset(),
                "LoginLatch");
            this.PresentFluidly<LoginPresenter>();
            w.Run<LatchHolder>(
                latch => latch.Latch.WaitOne(),
                "LoginLatch");
        }

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = "ui")
            where TPresenter : Presenter
        {
            var matchingPresenters = new LinkedList<Presenter>(
                this.presenters.Where(p => p is TPresenter));
            if (matchingPresenters.Count == 0)
            {
                return default(TUi);
            }

            if (presenterName == null)
            {
                return this.getUi<TUi>(
                    matchingPresenters.First.Value,
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

        private readonly LinkedList<Presenter> presenters;
        private readonly MethodWeb web;
    }
}
