namespace xofz.Misc.Framework
{
    public interface Loader
    {
        T Load<T>(string location);
    }
}
