namespace xofz.Framework.Plc.Implementation
{
    using System;
    using System.IO.Ports;
    using System.Text;

    internal sealed class SerialFpx : Fpx
    {
        private const string
            defaultUnitSpecifier = @"%01#",
            endCommandSpecifier = @"**";
        private const int
            readTimeout = 250,
            writeTimeout = 250;

        public SerialFpx(
            string portName,
            int baudRate)
        {
            byte portNumber;
            if (!portName.StartsWith("COM")
                || !byte.TryParse(
                    portName
                        .Substring(3),
                    out portNumber))
            {
                throw new InvalidOperationException(
                    "Cannot understand serial port \""
                    + portName + "\"");
            }

            this.portName = portName;
            this.baudRate = baudRate;
        }

        string Fpx.Location => this.portName;

        int Fpx.SecondaryLocation => this.baudRate;

        void Fpx.Read(string address, out bool bit)
        {
            this.checkAddress(address);
            var s = new StringBuilder()
                .Append(defaultUnitSpecifier)
                .Append("RCS")
                .Append(address[0])
                .Append(address.Substring(1).PadLeft(4, '0'))
                .Append(endCommandSpecifier)
                .ToString();
            var response = this.doOp(s);
            bit = response[response.IndexOf('C') + 1] == '1';
        }

        void Fpx.Read(string address, out short register)
        {
            this.checkAddress(address);
            if (!address.StartsWith("DT") &&
                !address.StartsWith("EV") &&
                !address.StartsWith("SV"))
            {
                throw new InvalidOperationException(
                    "The address for a short register must start with "
                    + "either DT, EV, or SV");
            }

            short location;
            var registerName = address.Substring(0, 2);
            if (!short.TryParse(
                address.Substring(2),
                out location))
            {
                throw new InvalidOperationException(
                    "Could not understand the location of the "
                    + registerName
                    + " register");
            }

            if (registerName == "DT")
            {
                register = BitConverter.ToInt16(
                    readBytes(this.dtValueString(location, location)),
                    0);
                return;
            }

            if (registerName == "SV")
            {
                register = BitConverter.ToInt16(
                    readBytes(this.svValueString(location)),
                    0);
                return;
            }

            if (registerName == "EV")
            {
                register = BitConverter.ToInt16(
                    readBytes(this.evValueString(location)),
                    0);
                return;
            }

            // should not ever get here
            register = default(short);
        }

        void Fpx.Read(string address, out ushort register)
        {
            this.checkAddress(address);
            if (!address.StartsWith("DT") &&
                !address.StartsWith("EV") &&
                !address.StartsWith("SV"))
            {
                throw new InvalidOperationException(
                    "The address for a ushort register must start with "
                    + "either DT, EV, or SV");
            }

            short location;
            var registerName = address.Substring(0, 2);
            if (!short.TryParse(
                address.Substring(2),
                out location))
            {
                throw new InvalidOperationException(
                    "Could not understand the location of the "
                    + registerName
                    + " register");
            }

            if (registerName == "DT")
            {
                register = BitConverter.ToUInt16(
                    readBytes(this.dtValueString(location, location)),
                    0);
                return;
            }

            if (registerName == "SV")
            {
                register = BitConverter.ToUInt16(
                    readBytes(this.svValueString(location)),
                    0);
                return;
            }

            if (registerName == "EV")
            {
                register = BitConverter.ToUInt16(
                    readBytes(this.evValueString(location)),
                    0);
                return;
            }

            // should not ever get here
            register = default(ushort);
        }

        void Fpx.Read(string address, out int register)
        {
            this.checkAddress(address);
            if (!address.StartsWith("DDT"))
            {
                throw new InvalidOperationException(
                    "The address for an int register must start with DDT.");
            }

            short location;
            if (!short.TryParse(
                address.Substring(3),
                out location))
            {
                throw new InvalidOperationException(
                    "Could not understand the location of the register.");
            }

            register = BitConverter.ToInt32(
                readBytes(this.ddtValueString(location)),
                0);
        }

        void Fpx.Read(string address, out uint register)
        {
            this.checkAddress(address);
            if (!address.StartsWith("DDT"))
            {
                throw new InvalidOperationException(
                    "The address for a uint register must start with DDT.");
            }

            short location;
            if (!short.TryParse(
                address.Substring(3),
                out location))
            {
                throw new InvalidOperationException(
                    "Could not understand the location of the register.");
            }

            register = BitConverter.ToUInt32(
                readBytes(this.ddtValueString(location)),
                0);
        }

        void Fpx.Read(string address, out float real)
        {
            this.checkAddress(address);
            if (!address.StartsWith("DDT"))
            {
                throw new InvalidOperationException(
                    "The address for a real register must start with DDT.");
            }

            short location;
            if (!short.TryParse(
                address.Substring(3),
                out location))
            {
                throw new InvalidOperationException(
                    "Could not understand the location of the register.");
            }

            real = BitConverter.ToSingle(
                readBytes(this.ddtValueString(location)),
                0);
        }

        void Fpx.Read(string address, out StringRegister register)
        {
            if (!address.StartsWith("DT"))
            {
                throw new InvalidOperationException(
                    "The address for a string register must start with DT.");
            }

            ushort start;
            if (!ushort.TryParse(
                address.Substring(2),
                out start))
            {
                throw new InvalidOperationException(
                    "Could not understand start location");
            }

            var maxLength = BitConverter.ToUInt16(
                readBytes(this.dtValueString(
                    start,
                    start)),
                0);
            var currentLength = BitConverter.ToUInt16(
                readBytes(this.dtValueString(
                    start + 1,
                    start + 1)),
                0);
            var numberOfRegisters = (currentLength + 1) / 2;

            var s = this.dtValueString(
                start + 2,
                start + 2 + numberOfRegisters - 1);
            var data = Encoding.ASCII.GetString(
                readBytes(s));
            if (currentLength % 2 == 1)
            {
                // remove the last character
                data = data.Substring(0, data.Length - 1);
            }

            register = new StringRegister(
                data,
                maxLength);
        }

        void Fpx.Write(string address, bool bit)
        {
            var s = new StringBuilder()
                .Append(defaultUnitSpecifier)
                .Append("WCS")
                .Append(address[0])
                .Append(address.Substring(1).PadLeft(4, '0'))
                .Append(bit ? '1' : '0')
                .Append(endCommandSpecifier)
                .ToString();
            this.doOp(s);
        }

        void Fpx.Write(string address, short register)
        {
            if (!address.StartsWith("DT")
                && !address.StartsWith("SV")
                && !address.StartsWith("EV"))
            {
                throw new ArgumentException(
                    "Address must start with DT, SV, or EV");
            }

            var sb = new StringBuilder();
            sb.Append(defaultUnitSpecifier);
            var location = string.Empty;
            switch (address.Substring(0, 2))
            {
                case "DT":
                    sb.Append("WDD");
                    location = address.Substring(2).PadLeft(5, '0');
                    break;
                case "SV":
                    sb.Append("WS");
                    location = address.Substring(2).PadLeft(4, '0');
                    break;
                case "EV":
                    sb.Append("WK");
                    location = address.Substring(2).PadLeft(4, '0');
                    break;
            }

            sb.Append(location);
            sb.Append(location);

            sb.Append(
                BitConverter.ToString(
                        BitConverter.GetBytes(register))
                    .Replace("-", string.Empty));
            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        void Fpx.Write(string address, ushort register)
        {
            if (!address.StartsWith("DT")
                && !address.StartsWith("SV")
                && !address.StartsWith("EV"))
            {
                throw new ArgumentException(
                    "Address must start with DT, SV, or EV");
            }

            var sb = new StringBuilder();
            sb.Append(defaultUnitSpecifier);
            var location = string.Empty;
            switch (address.Substring(0, 2))
            {
                case "DT":
                    sb.Append("WDD");
                    location = address.Substring(2).PadLeft(5, '0');
                    break;
                case "SV":
                    sb.Append("WS");
                    location = address.Substring(2).PadLeft(4, '0');
                    break;
                case "EV":
                    sb.Append("WK");
                    location = address.Substring(2).PadLeft(4, '0');
                    break;
            }

            sb.Append(location);
            sb.Append(location);

            sb.Append(
                BitConverter.ToString(
                        BitConverter.GetBytes(register))
                    .Replace("-", string.Empty));
            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        void Fpx.Write(string address, int register)
        {
            if (!address.StartsWith("DDT"))
            {
                throw new ArgumentException(
                    "Address must start with DDT.");
            }

            var sb = new StringBuilder();
            sb.Append(defaultUnitSpecifier);
            sb.Append("WDD");
            var location = int.Parse(address.Substring(3));
            sb.Append(location.ToString().PadLeft(5, '0'));
            sb.Append((location + 1).ToString().PadLeft(5, '0'));
            sb.Append(
                BitConverter.ToString(
                        BitConverter.GetBytes(register))
                    .Replace("-", string.Empty));
            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        void Fpx.Write(string address, uint register)
        {
            if (!address.StartsWith("DDT"))
            {
                throw new ArgumentException(
                    "Address must start with DDT.");
            }

            var sb = new StringBuilder();
            sb.Append(defaultUnitSpecifier);
            sb.Append("WDD");
            var location = int.Parse(address.Substring(3));
            sb.Append(location.ToString().PadLeft(5, '0'));
            sb.Append((location + 1).ToString().PadLeft(5, '0'));
            sb.Append(
                BitConverter.ToString(
                        BitConverter.GetBytes(register))
                    .Replace("-", string.Empty));
            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        void Fpx.Write(string address, float real)
        {
            if (!address.StartsWith("DDT"))
            {
                throw new ArgumentException(
                    "Address must start with DDT.");
            }

            var sb = new StringBuilder();
            sb.Append(defaultUnitSpecifier);
            sb.Append("WDD");
            var location = int.Parse(address.Substring(3));
            sb.Append(location.ToString().PadLeft(5, '0'));
            sb.Append((location + 1).ToString().PadLeft(5, '0'));
            sb.Append(
                BitConverter.ToString(
                        BitConverter.GetBytes(real))
                    .Replace("-", string.Empty));
            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        void Fpx.Write(string startAddress, string s)
        {
            if (!startAddress.StartsWith("DT"))
            {
                throw new ArgumentException(
                    "Start address must start with DT.");
            }

            StringRegister check;
            Fpx fpx = this;
            fpx.Read(startAddress, out check);
            var l = s.Length;
            var ml = check.MaxLength;
            if (l > ml)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(s),
                    @"The length of the string exceeds "
                    + @"the max length of the string register. "
                    + @"Length: " + l
                    + @" Max length: " + ml);
            }

            var start = ushort.Parse(
                            startAddress.Substring(2)) + 1;
            var numberOfRegisters = (s.Length + 1) / 2;
            var end = start + numberOfRegisters;

            var sb = new StringBuilder("<01#WDD");
            sb.Append(start.ToString().PadLeft(5, '0'));
            sb.Append(end.ToString().PadLeft(5, '0'));

            var lengthBytes = BitConverter.GetBytes(
                (ushort)s.Length);
            sb.Append(BitConverter
                .ToString(lengthBytes)
                .Replace("-", string.Empty));
            sb.Append(BitConverter
                .ToString(Encoding.ASCII.GetBytes(s))
                .Replace("-", string.Empty));

            // if there's an odd number of characters to write, add one more
            // (because each characters takes up only 1 byte and 
            // we can only write to the PLC 2 bytes at a time)
            if (s.Length % 2 == 1)
            {
                sb.Append("00");
            }

            sb.Append(endCommandSpecifier);
            this.doOp(sb.ToString());
        }

        string Fpx.Request(string command)
        {
            return this.doOp(command);
        }

        private string doOp(string op)
        {
            lock (this.locker)
            {
                using (var sp = new SerialPort(this.portName)
                {
                    BaudRate = this.baudRate,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Parity = Parity.Odd,
                    Handshake = Handshake.XOnXOff,
                    NewLine = "\r",
                    Encoding = Encoding.ASCII,
                    DtrEnable = true,
                    RtsEnable = true,
                    ReadTimeout = readTimeout,
                    WriteTimeout = writeTimeout,
                    ReadBufferSize = 65536,
                    WriteBufferSize = 65536
                })
                {
                    sp.Open();
                    sp.WriteLine(op);
                    return sp.ReadLine();
                }
            }
        }

        private string svValueString(short location)
        {
            if (location > 9999)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location must be 9999 or less.");
            }

            if (location < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location must be non-negative.");

            }

            var paddedLocation = location.ToString().PadLeft(4, '0');
            var s = new StringBuilder()
                .Append(defaultUnitSpecifier)
                .Append("RS")
                .Append(paddedLocation)
                .Append(paddedLocation)
                .Append(endCommandSpecifier)
                .ToString();
            var response = this.doOp(s);
            return response.Substring(
                6,
                response.Length - 6 - 2);
        }

        private string evValueString(short location)
        {
            if (location > 9999)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location must be 9999 or less.");
            }

            if (location < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location must be non-negative.");

            }

            var paddedLocation = location.ToString().PadLeft(4, '0');
            var s = new StringBuilder()
                .Append(defaultUnitSpecifier)
                .Append("RK")
                .Append(paddedLocation)
                .Append(paddedLocation)
                .Append(endCommandSpecifier)
                .ToString();
            var response = this.doOp(s);
            return response.Substring(
                6,
                response.Length - 6 - 2);
        }

        private string dtValueString(
            int start, int end)
        {
            if (start > 99999)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(start),
                    @"Start location too large");
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(start),
                    @"Start location must be non-negative.");
            }

            if (end > 99999)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(start),
                    @"End location too large");
            }

            if (end < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(start),
                    @"End location must be non-negative.");
            }

            var s = new StringBuilder()
                .Append("<01#RDD")
                .Append(start.ToString().PadLeft(5, '0'))
                .Append(end.ToString().PadLeft(5, '0'))
                .Append(endCommandSpecifier)
                .ToString();
            var response = this.doOp(s);
            return response.Substring(
                6,
                response.Length - 6 - 2);
        }

        private string ddtValueString(int location)
        {
            if (location > 99998)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location too large");
            }

            if (location < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(location),
                    @"Location must be non-negative.");
            }

            var paddedStartLocation = location.ToString().PadLeft(5, '0');
            ++location;
            var paddedEndLocation = location.ToString().PadLeft(5, '0');
            var s = new StringBuilder()
                .Append(defaultUnitSpecifier)
                .Append("RDD")
                .Append(paddedStartLocation)
                .Append(paddedEndLocation)
                .Append(endCommandSpecifier)
                .ToString();

            var response = this.doOp(s);
            return response.Substring(
                6,
                response.Length - 6 - 2);
        }

        private void checkAddress(string address)
        {
            if (address == null)
            {
                throw new InvalidOperationException(
                    "Could not understand address.");
            }

            if (address.Length < 2)
            {
                throw new InvalidOperationException(
                    "Address length too short.");
            }

            switch (address[0])
            {
                case 'E':
                    goto safe;
                case 'D':
                    goto safe;
                case 'R':
                    goto safe;
                case 'X':
                    goto safe;
                case 'Y':
                    goto safe;
                case 'S':
                    goto safe;
                default:
                    throw new InvalidOperationException(
                        "Could not understand start of address.");
            }

            safe:
            ;
        }

        private static byte[] readBytes(string hexString)
        {
            var l = hexString.Length;
            var bytes = new byte[(l + 1) / 2];
            for (var i = 0; i < l - 1; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(
                    hexString.Substring(i, 2), // value as string
                    16); // base
            }

            return bytes;
        }

        private readonly string portName;
        private readonly int baudRate;
        private readonly object locker = new object();
    }
}
