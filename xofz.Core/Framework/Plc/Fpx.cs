// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fpx.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace xofz.Framework.Plc
{
    public interface Fpx
    {
        int ReadTimeout { get; set; }

        string Location { get; }

        bool ReadBit(string address);

        void WriteBit(string address, bool value);

        short ReadShortRegister(string address);

        ushort ReadUShortRegister(string address);

        int ReadIntRegister(string address);

        uint ReadUIntRegister(string address);

        float ReadFloatRegister(string address);

        StringData ReadString(string startAddress);

        void WriteRegister(string address, short value);

        void WriteRegister(string address, ushort value);

        void WriteRegister(string address, int value);

        void WriteRegister(string address, uint value);

        void WriteRegister(string address, float value);

        void WriteString(string startAddress, string value);

        string Do(string command);

        void Disconnect();
    }
}
