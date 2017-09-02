namespace xofz.Framework.Plc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;

    public class TcpFpx : Fpx, IDisposable
    {
        public TcpFpx(string hostNameOrIp, int port = 9094)
        {
            this.hostNameOrIp = hostNameOrIp;
            this.port = port;
            this.ReadTimeout = 2500;
            this.WriteTimeout = 2500;
        }

        public int ReadTimeout { get; set; }

        public int WriteTimeout { get; set; }

        public int Port => this.port;

        public void Disconnect()
        {
            lock (this.locker)
            {
                this.stream?.Dispose();
                this.connection?.Client?.Close();
                this.connection?.Close();
            }
        }

        public string Location => this.hostNameOrIp;

        public bool ReadBit(string address)
        {
            var sb = new StringBuilder();
            sb.Append("%01#RCS");
            sb.Append(address[0]);
            sb.Append(address.Substring(1).PadLeft(4, '0'));
            sb.Append("**");

            var response = this.performCommand(sb.ToString());
            return response[response.IndexOf('C') + 1] == '1';
        }

        public void WriteBit(string address, bool value)
        {
            var sb = new StringBuilder();
            sb.Append("%01#WCS");
            sb.Append(address[0]);
            sb.Append(address.Substring(1).PadLeft(4, '0'));
            sb.Append(value ? '1' : '0');
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        public short ReadShortRegister(string address)
        {
            if (address.StartsWith("DT"))
            {
                var a = int.Parse(address.Substring(2));
                return BitConverter.ToInt16(
                    hexStringToBytes(
                        this.dtValueString(a, a)), 0);
            }

            if (address.StartsWith("SV"))
            {
                return BitConverter.ToInt16(
                    hexStringToBytes(
                        this.svValueString(address)), 0);
            }

            if (address.StartsWith("EV"))
            {
                return BitConverter.ToInt16(
                    hexStringToBytes(
                        this.evValueString(address)), 0);
            }

            throw new ArgumentException(
                "Address did not start with DT, SV, or EV");
        }

        public ushort ReadUShortRegister(string address)
        {
            if (address.StartsWith("DT"))
            {
                var a = int.Parse(address.Substring(2));
                return BitConverter.ToUInt16(
                    hexStringToBytes(
                        this.dtValueString(a, a)), 0);
            }

            if (address.StartsWith("SV"))
            {
                return BitConverter.ToUInt16(
                    hexStringToBytes(
                        this.svValueString(address)), 0);
            }

            if (address.StartsWith("EV"))
            {
                return BitConverter.ToUInt16(
                    hexStringToBytes(
                        this.evValueString(address)), 0);
            }

            throw new ArgumentException("Address did not start with DT, SV, or EV");
        }

        public int ReadIntRegister(string address)
        {
            return BitConverter.ToInt32(
                hexStringToBytes(
                    this.ddtValueString(address)), 0);
        }

        public uint ReadUIntRegister(string address)
        {
            return BitConverter.ToUInt32(
                hexStringToBytes(
                    this.ddtValueString(address)), 0);
        }

        public float ReadFloatRegister(string address)
        {
            return BitConverter.ToSingle(
                hexStringToBytes(
                    this.ddtValueString(address)), 0);
        }

        public StringData ReadString(string startAddress)
        {
            var start = ushort.Parse(startAddress
                .Substring(2)); // string addresses start with DT
            var maxLength = BitConverter.ToUInt16(
                hexStringToBytes(this.dtValueString(start, start)),
                0);
            ++start;
            var currentLength = BitConverter.ToUInt16(
                hexStringToBytes(
                    this.dtValueString(start, start)), 0);
            var numberOfRegisters = (currentLength + 1) / 2;
            ++start;

            // end address is start + number - 1, because it starts right at start, not at start + 1
            var s = this.dtValueString(start, start + numberOfRegisters - 1);

            var data = Encoding.ASCII.GetString(hexStringToBytes(s));
            if (currentLength % 2 == 1)
            {
                // remove the last character
                data = data.Substring(0, data.Length - 1);
            }

            return new StringData(maxLength, currentLength, data);
        }

        public void WriteRegister(string address, short value)
        {
            var sb = new StringBuilder();
            sb.Append("%01#");
            if (address.StartsWith("DT"))
            {
                sb.Append("WDD");
                var addressValue = address.Substring(2).PadLeft(5, '0');
                this.write(sb, addressValue, value);

                return;
            }

            if (address.StartsWith("SV"))
            {
                sb.Append("WS");
                var addressValue = address.Substring(2).PadLeft(4, '0');
                this.write(sb, addressValue, value);

                return;
            }

            if (address.StartsWith("EV"))
            {
                sb.Append("WK");
                var addressValue = address.Substring(2).PadLeft(4, '0');
                this.write(sb, addressValue, value);

                return;
            }

            throw new ArgumentException(
                "Address did not start with DT, SV, or EV");
        }

        public void WriteRegister(string address, ushort value)
        {
            var sb = new StringBuilder("%01#");
            if (address.StartsWith("DT"))
            {
                sb.Append("WDD");
                var addressValue = address.Substring(2).PadLeft(5, '0');
                this.write(sb, addressValue, value);

                return;
            }

            if (address.StartsWith("SV"))
            {
                sb.Append("WS");
                var addressValue = address.Substring(2).PadLeft(4, '0');
                this.write(sb, addressValue, value);

                return;
            }

            if (address.StartsWith("EV"))
            {
                sb.Append("WK");
                var addressValue = address.Substring(2).PadLeft(4, '0');
                this.write(sb, addressValue, value);

                return;
            }

            throw new ArgumentException(
                "Address did not start with DT, SV, or EV");
        }

        public void WriteRegister(string address, int value)
        {
            var sb = new StringBuilder("%01#WDD");
            var addressValue = address.Substring(3).PadLeft(5, '0');
            sb.Append(addressValue);
            sb.Append((ushort.Parse(addressValue) + 1).ToString().PadLeft(5, '0'));
            var valueBytes = BitConverter.GetBytes(value);
            var hex = BitConverter.ToString(valueBytes).Replace("-", string.Empty);
            sb.Append(hex);
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        public void WriteRegister(string address, uint value)
        {
            var sb = new StringBuilder("%01#WDD");
            var addressValue = address.Substring(3).PadLeft(5, '0');
            sb.Append(addressValue);
            sb.Append((ushort.Parse(addressValue) + 1).ToString().PadLeft(5, '0'));
            var valueBytes = BitConverter.GetBytes(value);
            var hex = BitConverter.ToString(valueBytes).Replace("-", string.Empty);
            sb.Append(hex);
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        public void WriteRegister(string address, float value)
        {
            var sb = new StringBuilder("%01#WDD");
            var addressValue = address.Substring(3).PadLeft(5, '0');
            sb.Append(addressValue);
            sb.Append((ushort.Parse(addressValue) + 1).ToString().PadLeft(5, '0'));
            var valueBytes = BitConverter.GetBytes(value);
            var hex = BitConverter.ToString(valueBytes).Replace("-", string.Empty);
            sb.Append(hex);
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        public void WriteString(string startAddress, string value)
        {
            var data = this.ReadString(startAddress);
            if (value.Length > data.MaxLength)
            {
                const string message
                    = "Value length must be less than "
                      + "or equal to the max length of the string. ";
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    message
                    + @"value length: "
                    + value.Length
                    + @"; max length: "
                    + data.MaxLength);
            }

            var start = ushort.Parse(startAddress
                .Substring(2)); // string addresses start with DT
            ++start;
            var numberOfRegisters = (value.Length + 1) / 2;
            var sb = new StringBuilder("<01#WDD");
            sb.Append(start.ToString().PadLeft(5, '0'));
            var end = start + numberOfRegisters;
            sb.Append(end.ToString().PadLeft(5, '0'));

            var lengthBytes = BitConverter.GetBytes(
                (ushort)value.Length);
            sb.Append(BitConverter.ToString(lengthBytes)
                .Replace("-", string.Empty));
            sb.Append(BitConverter.ToString(Encoding.ASCII.GetBytes(value))
                .Replace("-", string.Empty));

            // if there's an odd number of characters to write, add one more
            // (because each characters takes up only 1 byte and 
            // we can only write to the PLC 2 bytes at a time)
            if (value.Length % 2 == 1)
            {
                sb.Append("00");
            }

            sb.Append("**");
            var s = sb.ToString();
            var response = this.performCommand(s);
            if (response.Contains("!"))
            {
                throw new InvalidOperationException(
                    "An error occurred: " + response);
            }
        }

        /// <summary>
        /// Connects to the PLC, performs the command, disconnects from the PLC, and returns the response.
        /// This method ensures the PLC is disconnected before the method finishes, unlike the Read/Write methods.
        /// </summary>
        public string Do(string command)
        {
            string response;
            try
            {
                response = this.performCommand(command);
            }
            finally
            {
                this.Disconnect();
            }

            return response;
        }

        public void Dispose()
        {
            this.Disconnect();
        }

        private void connect()
        {
            if (this.connection != null && this.connection.Connected)
            {
                this.connection.ReceiveTimeout = this.ReadTimeout;
                this.connection.SendTimeout = this.WriteTimeout;
                return;
            }

            this.connection = new TcpClient
            {
                ReceiveTimeout = this.ReadTimeout,
                SendTimeout = this.WriteTimeout
            };
            this.connection.Connect(this.hostNameOrIp, this.port);
            this.stream = this.connection.GetStream();
        }

        private string performCommand(string command)
        {
            lock (this.locker)
            {
                this.connect();
                var bytes = Encoding.ASCII.GetBytes(command + '\r');
                try
                {
                    this.stream.Write(bytes, 0, bytes.Length);
                }
                catch (IOException)
                {
                    throw new TimeoutException(
                        "Timeout while sending command to FP-X @"
                        + this.Location + ".");
                }

                var response = new List<byte>();
                try
                {
                    response.Add(
                        Convert.ToByte(
                            this.stream.ReadByte()));
                }
                catch (IOException)
                {
                    throw new TimeoutException(
                        "Timeout while reading FP-X response @" 
                        + this.Location + ".");
                }

                while (this.stream.DataAvailable)
                {
                    response.Add(Convert.ToByte(this.stream.ReadByte()));
                }

                return Encoding.ASCII.GetString(response.ToArray());
            }
        }

        private string dtValueString(int startAddress, int endAddress)
        {
            var sb = new StringBuilder();
            sb.Append("<01#RDD");
            sb.Append(startAddress.ToString().PadLeft(5, '0'));
            sb.Append(endAddress.ToString().PadLeft(5, '0'));
            sb.Append("**");
            var response = this.performCommand(sb.ToString());
            var value = response.Substring(6);
            return value.Remove(value.Length - 2, 2);
        }

        private string svValueString(string address)
        {
            var sb = new StringBuilder();
            sb.Append("%01#RS");
            var addressValue = address.Substring(2).PadLeft(4, '0');
            sb.Append(addressValue);
            sb.Append(addressValue);
            sb.Append("**");
            var response = this.performCommand(sb.ToString());
            var value = response.Substring(6);
            return value.Remove(value.Length - 2, 2);
        }

        private string evValueString(string address)
        {
            var sb = new StringBuilder();
            sb.Append("%01#RK");
            var addressValue = address.Substring(2).PadLeft(4, '0');
            sb.Append(addressValue);
            sb.Append(addressValue);
            sb.Append("**");
            var response = this.performCommand(sb.ToString());
            var value = response.Substring(6);
            return value.Remove(value.Length - 2, 2);
        }

        private string ddtValueString(string address)
        {
            var sb = new StringBuilder();
            sb.Append("%01#RDD");
            sb.Append(address.Substring(3).PadLeft(5, '0'));
            var addressValue = int.Parse(address.Substring(3));
            ++addressValue;
            sb.Append(addressValue.ToString().PadLeft(5, '0'));
            sb.Append("**");
            var response = this.performCommand(sb.ToString());
            var value = response.Substring(6);
            return value.Remove(value.Length - 2, 2);
        }

        private void write(
            StringBuilder sb,
            string addressValue, 
            short value)
        {
            sb.Append(addressValue);
            sb.Append(addressValue);
            var valueBytes = BitConverter.GetBytes(value);
            var hex = BitConverter.ToString(valueBytes).Replace("-", string.Empty);
            sb.Append(hex);
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        private void write(
            StringBuilder sb, 
            string addressValue, 
            ushort value)
        {
            sb.Append(addressValue);
            sb.Append(addressValue);
            var valueBytes = BitConverter.GetBytes(value);
            var hex = BitConverter.ToString(valueBytes)
                .Replace("-", string.Empty);
            sb.Append(hex);
            sb.Append("**");

            this.performCommand(sb.ToString());
        }

        private static byte[] hexStringToBytes(string s)
        {
            var bytes = new byte[(s.Length + 1) / 2];
            for (var i = 0; i < s.Length - 1; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            }

            return bytes;
        }

        private NetworkStream stream;
        private TcpClient connection;
        private readonly string hostNameOrIp;
        private readonly int port;
        private readonly object locker = new object();
    }
}
