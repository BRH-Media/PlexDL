using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("76B1BBDB-4EC8-4F36-B106-70A9316DF593")]
    internal interface IMFAudioStreamVolume
    {
        [PreserveSig]
        HResult GetChannelCount(
            out int pdwCount
        );

        [PreserveSig]
        HResult SetChannelVolume(
            int dwIndex,
            float fLevel
        );

        [PreserveSig]
        HResult GetChannelVolume(
            int dwIndex,
            out float pfLevel
        );

        [PreserveSig]
        HResult SetAllVolumes(
            int dwCount,
            [In] [MarshalAs(UnmanagedType.LPArray)]
            float[] pfVolumes
        );

        [PreserveSig]
        HResult GetAllVolumes(
            int dwCount,
            [Out] [MarshalAs(UnmanagedType.LPArray)]
            float[] pfVolumes
        );
    }
}