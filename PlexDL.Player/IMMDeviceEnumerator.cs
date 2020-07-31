using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMDeviceEnumerator
    {
        [PreserveSig]
        void EnumAudioEndpoints([ComAliasName("MMDevAPI.Interop.EDataFlow")][In] EDataFlow dataFlow, [In] uint dwStateMask, [MarshalAs(UnmanagedType.Interface)] out IMMDeviceCollection ppDevices);

        [PreserveSig]
        void GetDefaultAudioEndpoint([ComAliasName("MMDevAPI.Interop.EDataFlow")][In] EDataFlow dataFlow, [ComAliasName("MMDevAPI.Interop.ERole")][In] ERole role, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppEndpoint);

        [PreserveSig]
        void GetDevice([MarshalAs(UnmanagedType.LPWStr)][In] string pwstrId, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppDevice);

        [PreserveSig]
        void RegisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)][In] IMMNotificationClient pClient);

        [PreserveSig]
        void UnregisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)][In] IMMNotificationClient pClient);
    }
}