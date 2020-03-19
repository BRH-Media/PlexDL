using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [TypeLibType(TypeLibTypeFlags.FNonExtensible)]
    [ComImport]
    internal interface IMMDeviceCollection
    {
        [PreserveSig]
        void GetCount(out uint pcDevices);

        [PreserveSig]
        void Item([In] uint nDevice, [MarshalAs(UnmanagedType.Interface)] out IMMDevice ppDevice);
    }
}