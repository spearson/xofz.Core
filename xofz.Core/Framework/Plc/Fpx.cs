namespace xofz.Framework.Plc
{
    public interface Fpx
    {
        string Location { get; }

        int SecondaryLocation { get; }

        void Read(string address, out bool bit);

        void Read(string address, out short register);

        void Read(string address, out ushort register);

        void Read(string address, out int register);

        void Read(string address, out uint register);

        void Read(string address, out float real);

        void Read(string startAddress, out StringRegister register);

        void Write(string address, bool bit);

        void Write(string address, short register);

        void Write(string address, ushort register);

        void Write(string address, int register);

        void Write(string address, uint register);

        void Write(string address, float real);

        void Write(string startAddress, string s);

        string Request(string command);
    }
}
