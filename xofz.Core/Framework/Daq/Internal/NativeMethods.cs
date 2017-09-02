namespace xofz.Framework.Daq.Internal
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern SafeFileHandle CreateFile(
            [In] string fileName,
            [In] [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess,
            [In] [MarshalAs(UnmanagedType.U4)] FileShare fileShare,
            [In] [Optional] IntPtr securityAttributes,
            [In] [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [In] [MarshalAs(UnmanagedType.U4)] FileAttributes flags,
            [In] [Optional] IntPtr template);

        [DllImport(@"setupapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiEnumDeviceInterfaces(
            [In] DevInfoSetHandle deviceInfoSet,
            [In] [Optional] IntPtr deviceInfoData,
            [In] ref Guid interfaceClassGuid,
            [In] uint memberIndex,
            [In] [Out] ref DeviceInterfaceData deviceInterfaceData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(
            [In] DevInfoSetHandle deviceInfoSet,
            [In] ref DeviceInterfaceData deviceInterfaceData,
            [In] [Out] [Optional] ref DeviceInterfaceDetailData details,
            [In] int deviceInterfaceDetailDataSize,
            [Out] [Optional] out int requiredSize,
            [Out] [Optional] IntPtr deviceInfoData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(
            [In] DevInfoSetHandle deviceInfoSet,
            [In] ref DeviceInterfaceData deviceInterfaceData,
            [Out] [Optional] IntPtr deviceInterfaceDetailData,
            [In] int deviceInterfaceDetailDataSize,
            [Out] [Optional] out int requiredSize,
            [Out] [Optional] IntPtr deviceInfoData);

        [DllImport("setupapi.dll")]
        public static extern DevInfoSetHandle SetupDiGetClassDevs(
            [In] ref Guid classGuid,
            [In] IntPtr enumerator,
            [In] IntPtr hwndParent,
            [In] DeviceFilters flags);

        [DllImport(@"setupapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr devInfo);

        [DllImport("winusb.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinUsb_Initialize(
            SafeFileHandle deviceHandle,
            out DeviceInterfaceHandle deviceInterfaceHandle);

        [DllImport("winusb.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinUsb_Free([In] IntPtr interfaceHandle);

        [DllImport("winusb.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinUsb_ControlTransfer(
            [In] DeviceInterfaceHandle deviceInterfaceHandle,
            [In] SetupPacket setupPacket,
            [In] ref byte buffer,
            [In] uint bufferLength,
            [Out] [Optional] out uint lengthTransferred,
            [In] [Optional] IntPtr overlapped);
    }
}
