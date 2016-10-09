namespace xofz.Presentation
{
    public class EventRaiser
    {
        public virtual void Raise(object eventHolder, string eventName, params object[] args)
        {
            var e = eventHolder.GetType().GetEvent(eventName);
            e.GetRaiseMethod().Invoke(eventHolder, args);
        }
    }
}
