namespace xofz.Framework.Modbus.Internal
{
    internal sealed class TcpController : Controller
    {
        public string Location { get; set; }

        public int? SecondaryLocation { get; set; }

        ushort[] Controller.ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                return connection.ReadHoldingRegisters(
                    startAddress, 
                    numberOfRegisters);
            }
        }

        ushort[] Controller.ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                return connection.ReadInputRegisters(
                    startAddress,
                    numberOfRegisters);
            }
        }

        bool[] Controller.ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                return connection.ReadCoils(
                    startAddress,
                    numberOfCoils);
            }
        }

        bool[] Controller.ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                return connection.ReadInputs(
                    startAddress,
                    numberOfInputs);
            }
        }

        void Controller.WriteSingleCoil(
            ushort address, 
            bool value)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                connection.WriteSingleCoil(
                    address,
                    value);
            }
        }

        void Controller.WriteMultipleCoils(
            ushort startAddress,
            bool[] values)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                connection.WriteMultipleCoils(
                    startAddress,
                    values);
            }
        }

        void Controller.WriteSingleHoldingRegister(
            ushort address, 
            ushort value)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                connection.WriteSingleHoldingRegister(
                    address,
                    value);
            }
        }

        void Controller.WriteMultipleHoldingRegisters(
            ushort startAddress, 
            ushort[] values)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation ?? 502))
            {
                connection.WriteMultipleHoldingRegisters(
                    startAddress,
                    values);
            }
        }
    }
}
