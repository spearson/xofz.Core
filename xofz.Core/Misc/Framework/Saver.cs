namespace xofz.Misc.Framework
{
    public interface Saver
    {
        void Save<T>(string location, T value);
    }
}
