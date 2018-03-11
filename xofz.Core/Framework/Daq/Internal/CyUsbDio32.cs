﻿namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.IO;
    using System.Linq;

    internal sealed class CyUsbDio32 : Dio32
    {
        public CyUsbDio32(uint deviceIndex)
        {
            this.deviceIndex = deviceIndex;
        }

        string Dio32.DeviceLocation 
            => this.deviceIndex.ToString();

        string Dio32.ReadSerialNumber()
        {
            ulong serialNumber;
            NativeMethods.GetDeviceSerialNumber(
                this.deviceIndex,
                out serialNumber);
            var hex = serialNumber.ToString("x");

            return string.Join(
                "-",
                StringHelpers.Chunks(
                    hex.PadLeft(16, '0'),
                    4));
        }

        Dio32Terminals Dio32.ReadOnTerminals()
        {
            uint data;
            NativeMethods.DIO_ReadAll(
                this.deviceIndex,
                out data);
            var onTerminals = Dio32Terminals.None;
            for (byte i = 0; i < 32; ++i)
            {
                if (data % 2 == 1)
                {
                    onTerminals |= getTerminalFromIndex(i);
                }

                data >>= 1;
            }

            return onTerminals;
        }

        void Dio32.WriteTerminals(Dio32Terminals newOnTerminals)
        {
            uint data = 0b00000000_00000000_00000000_00000000;
            byte onTerminalCounter = 0;
            foreach (var terminal in EnumHelpers
                .Iterate<Dio32Terminals>())
            {
                if (isNonePortOrAll(terminal))
                {
                    continue;
                }

                if (newOnTerminals.HasFlag(terminal))
                {
                    data += (uint)(
                        Math.Pow(2, onTerminalCounter)
                        + 0.00001);
                }

                ++onTerminalCounter;
            }

            NativeMethods.DIO_WriteAll(
                this.deviceIndex,
                ref data);
        }

        void Dio32.Configure(Dio32Terminals onTerminals, Dio32Ports outputs)
        {
            byte outMask = 0;
            byte outputCounter = 0;
            foreach (var output in EnumHelpers
                .Iterate<Dio32Ports>())
            {
                if (isNoneOrAll(output))
                {
                    continue;
                }

                if (outputs.HasFlag(output))
                {
                    outMask += (byte)(
                        Math.Pow(2, outputCounter)
                        + 0.00001);
                }

                ++outputCounter;
            }

            uint data = 0b00000000_00000000_00000000_00000000;
            byte onTerminalCounter = 0;
            foreach (var terminal in EnumHelpers
                .Iterate<Dio32Terminals>())
            {
                if (isNonePortOrAll(terminal))
                {
                    continue;
                }

                if (onTerminals.HasFlag(terminal))
                {
                    data += (uint)(
                        Math.Pow(2, onTerminalCounter)
                        + 0.00001);
                }

                ++onTerminalCounter;
            }

            NativeMethods.DIO_Configure(
                this.deviceIndex,
                1,
                ref outMask,
                ref data);
        }

        private static Dio32Terminals getTerminalFromIndex(byte index)
        {
            switch (index)
            {
                case 0:
                    return Dio32Terminals.A0;
                case 1:
                    return Dio32Terminals.A1;
                case 2:
                    return Dio32Terminals.A2;
                case 3:
                    return Dio32Terminals.A3;
                case 4:
                    return Dio32Terminals.A4;
                case 5:
                    return Dio32Terminals.A5;
                case 6:
                    return Dio32Terminals.A6;
                case 7:
                    return Dio32Terminals.A7;
                case 8:
                    return Dio32Terminals.B0;
                case 9:
                    return Dio32Terminals.B1;
                case 10:
                    return Dio32Terminals.B2;
                case 11:
                    return Dio32Terminals.B3;
                case 12:
                    return Dio32Terminals.B4;
                case 13:
                    return Dio32Terminals.B5;
                case 14:
                    return Dio32Terminals.B6;
                case 15:
                    return Dio32Terminals.B7;
                case 16:
                    return Dio32Terminals.C0;
                case 17:
                    return Dio32Terminals.C1;
                case 18:
                    return Dio32Terminals.C2;
                case 19:
                    return Dio32Terminals.C3;
                case 20:
                    return Dio32Terminals.C4;
                case 21:
                    return Dio32Terminals.C5;
                case 22:
                    return Dio32Terminals.C6;
                case 23:
                    return Dio32Terminals.C7;
                case 24:
                    return Dio32Terminals.D0;
                case 25:
                    return Dio32Terminals.D1;
                case 26:
                    return Dio32Terminals.D2;
                case 27:
                    return Dio32Terminals.D3;
                case 28:
                    return Dio32Terminals.D4;
                case 29:
                    return Dio32Terminals.D5;
                case 30:
                    return Dio32Terminals.D6;
                case 31:
                    return Dio32Terminals.D7;
                default:
                    return Dio32Terminals.None;
            }
        }

        private static bool isNonePortOrAll(Dio32Terminals terminals)
        {
            if (terminals == Dio32Terminals.None)
            {
                return true;
            }
            
            if (terminals == Dio32Terminals.PortA)
            {
                return true;
            }

            if (terminals == Dio32Terminals.PortB)
            {
                return true;
            }

            if (terminals == Dio32Terminals.PortC)
            {
                return true;
            }

            if (terminals == Dio32Terminals.PortD)
            {
                return true;
            }

            if (terminals == Dio32Terminals.All)
            {
                return true;
            }

            return false;
        }

        private static bool isNoneOrAll(Dio32Ports ports)
        {
            if (ports == Dio32Ports.None)
            {
                return true;
            }

            if (ports == Dio32Ports.All)
            {
                return true;
            }

            return false;
        }

        private readonly uint deviceIndex;
    }
}
