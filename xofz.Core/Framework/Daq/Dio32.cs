namespace xofz.Framework.Daq
{
    public interface Dio32
    {
        string Location { get; }

        byte SecondaryLocation { get; }

        string ReadSerialNumber();

        Dio32Terminals ReadOnTerminals();

        void WriteTerminals(Dio32Terminals newOnTerminals);

        void Configure(Dio32Terminals onTerminals, Dio32Ports outputs);
    }
}