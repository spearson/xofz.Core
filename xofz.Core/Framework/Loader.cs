namespace xofz.Framework
{
    public interface Loader
    {
        T Load<T>(string location);
    }
}
