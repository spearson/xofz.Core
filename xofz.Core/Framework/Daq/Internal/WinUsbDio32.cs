namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    internal sealed class WinUsbDio32 : Dio32
    {
        public WinUsbDio32(string devicePath)
        {
            this.devicePath = devicePath;
            this.locker = new object();
        }

        string Dio32.Location => this.devicePath;

        byte Dio32.SecondaryLocation => 0;

        string Dio32.ReadSerialNumber()
        {
            var result = this.controlTransfer(
                0xC0, 
                0xA2, 
                0x1DF8, 
                new byte[8]);
            if (this.errorOccurred)
            {
                throw new IOException(
                    "Error reading serial number.  Error code: "
                    + Marshal.GetLastWin32Error());
            }

            var value = BitConverter.ToUInt64(result, 0);
            var hex = value.ToString("x");
            if (hex.Length > 16)
            {
                hex = hex.Substring(hex.Length - 16);
            }

            var serialNumber = string.Join(
                "-",
                StringHelpers.Chunks(
                    hex.PadLeft(16, '0'),
                    4));
            return serialNumber;
        }

        Dio32Terminals Dio32.ReadOnTerminals()
        {
            var result = this.controlTransfer(
                0xC0, 
                0x11, 
                0, 
                new byte[4]);
            if (this.errorOccurred)
            {
                throw new IOException(
                    "Error reading on terminals. Error code: "
                    + Marshal.GetLastWin32Error());
            }

            var value = ~BitConverter.ToUInt32(result, 0);
            var terminals = (Dio32Terminals)value;
            return terminals;
        }

        void Dio32.WriteTerminals(Dio32Terminals newOnTerminals)
        {
            this.controlTransfer(
                0x40, 
                0x10, 
                0, 
                BitConverter.GetBytes(~(uint)newOnTerminals));
            if (this.errorOccurred)
            {
                throw new IOException(
                    "Error writing terminals to the USB-DIO-32. Error code: "
                    + Marshal.GetLastWin32Error());
            }
        }

        void Dio32.Configure(Dio32Terminals onTerminals, Dio32Ports outputs)
        {
            var data = new byte[17];
            var value = ~(uint)onTerminals;
            BitConverter.GetBytes(value).CopyTo(data, 0);
            data[4] = (byte)outputs;

            this.controlTransfer(0x40, 0x12, 0, data);
            if (this.errorOccurred)
            {
                throw new IOException(
                    "Error configuring the USB-DIO-32. Error code: "
                    + Marshal.GetLastWin32Error());
            }
        }

        private byte[] controlTransfer(byte requestType, byte request, ushort requestParameter, byte[] buffer)
        {
            var setupPacket = new SetupPacket
            {
                RequestType = requestType,
                Request = request,
                Value = requestParameter,
                Index = 0,
                Length = (ushort)buffer.Length
            };

            uint bytesTransferred;
            DeviceInterfaceHandle interfaceHandle;
            lock (this.locker)
            {
                this.errorOccurred = false;
                using (var deviceHandle = NativeMethods.CreateFile(
                    this.devicePath,
                    FileAccess.ReadWrite,
                    FileShare.None,
                    IntPtr.Zero,
                    FileMode.Open,
                    Internal.FileAttributes.Overlapped,
                    IntPtr.Zero))
                {
                    NativeMethods.WinUsb_Initialize(deviceHandle, out interfaceHandle);
                    using (interfaceHandle)
                    {
                        if (!NativeMethods.WinUsb_ControlTransfer(
                            interfaceHandle,
                            setupPacket,
                            ref buffer[0],
                            setupPacket.Length,
                            out bytesTransferred,
                            IntPtr.Zero))
                        {
                            this.errorOccurred = true;
                        }
                    }
                }
            }

            var result = new byte[bytesTransferred];
            Array.Copy(buffer, result, bytesTransferred);

            return result;
        }

        private bool errorOccurred;
        private readonly string devicePath;
        private readonly object locker;
    }
}
