namespace xofz.Framework.Plc
{
    public interface AbPlc
    {
        string Read(string address);

        void Write(string address, string value);
    }
}
