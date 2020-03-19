using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMNotificationClient
    {
        [PreserveSig]
        void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId, [In] DeviceState dwNewState);

        [PreserveSig]
        void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId);

        [PreserveSig]
        void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId);

        [PreserveSig]
        void OnDefaultDeviceChanged([ComAliasName("MMDevAPI.Interop.EDataFlow")] [In]
            EDataFlow flow, [ComAliasName("MMDevAPI.Interop.ERole")] [In]
            ERole role, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDefaultDeviceId);

        [PreserveSig]
        void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string pwstrDeviceId, [In] PropertyKey key);
    }
}