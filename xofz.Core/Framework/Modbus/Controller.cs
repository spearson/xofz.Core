namespace xofz.Framework.Modbus
{
    using System;

    public interface Controller
    {
        string Location { get; set; }

        Nullable<int> SecondaryLocation { get; set; }

        ushort[] ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters);

        ushort[] ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters);

        bool[] ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils);

        bool[] ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs);

        void WriteSingleCoil(
            ushort address, 
            bool value);

        void WriteMultipleCoils(
            ushort startAddress, 
            bool[] values);

        void WriteSingleHoldingRegister(
            ushort address, 
            ushort value);

        void WriteMultipleHoldingRegisters(
            ushort startAddress, 
            ushort[] values);
    }
}
