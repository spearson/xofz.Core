namespace xofz.Framework.Plc
{
    public interface AbPlc
    {
        string Location { get; }

        string Read(string address);

        void Write(string address, string value);
    }
}
