﻿namespace xofz.Framework.Modbus.Internal
{
    using System.IO.Ports;

    internal sealed class SerialController : Controller
    {
        public SerialController(
            SerialPort port,
            SerialMode mode)
        {
            this.port = port;
            this.mode = mode;
        }

        string Controller.Location
        {
            get => this.port?.PortName;
            set { }
        }

        int? Controller.SecondaryLocation
        {
            get { return null; }
            set { }
        }

        ushort[] Controller.ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
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
            using (var connection = new SerialNModbusConnection(
                this.port,
                this.mode))
            {
                connection.WriteMultipleHoldingRegisters(
                    startAddress,
                    values);
            }
        }

        private readonly SerialPort port;
        private readonly SerialMode mode;
    }
}
