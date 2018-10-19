namespace xofz.Misc.Framework.Martyr
{
    using System;
    using xofz.Misc.Framework.Illumination;
    using xofz.Framework.Lots;

    public class PrescientInvoker
    {
        public PrescientInvoker(
            Illuminator illuminator, 
            FreedomHolder freedomHolder)
        {
            this.illuminator = illuminator;
            this.freedomHolder = freedomHolder;
        }

        public virtual T PreInvoke<T>(
            Action<T> act, 
            params object[] dependencies) 
            where T : class, IDisposable
        {
            var actor = this.illuminator.Illumine<T>(dependencies);
            act(actor);
            this.freedomHolder.Surge(
                new LinkedListLot<IDisposable>(
                    new IDisposable[] { actor }));
            return actor;
        }

        private readonly Illuminator illuminator;
        private readonly FreedomHolder freedomHolder;
    }
}
