using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("089EDF13-CF71-4338-8D13-9E569DBDC319"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFSimpleAudioVolume
    {
        [PreserveSig]
        HResult SetMasterVolume(
            [In] float fLevel
        );

        [PreserveSig]
        HResult GetMasterVolume(
            out float pfLevel
        );

        [PreserveSig]
        HResult SetMute(
            [In, MarshalAs(UnmanagedType.Bool)] bool bMute
        );

        [PreserveSig]
        HResult GetMute(
            [MarshalAs(UnmanagedType.Bool)] out bool pbMute
        );
    }
}