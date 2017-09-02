namespace xofz.Misc
{
    public interface Beholder<in T>
    {
        void Receive(T state);
    }
}
