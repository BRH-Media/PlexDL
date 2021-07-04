using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("F6696E82-74F7-4F3D-A178-8A5E09C3659F")]
    internal interface IMFClockStateSink
    {
        [PreserveSig]
        HResult OnClockStart(
            [In] long hnsSystemTime,
            [In] long llClockStartOffset
        );

        [PreserveSig]
        HResult OnClockStop(
            [In] long hnsSystemTime
        );

        [PreserveSig]
        HResult OnClockPause(
            [In] long hnsSystemTime
        );

        [PreserveSig]
        HResult OnClockRestart(
            [In] long hnsSystemTime
        );

        [PreserveSig]
        HResult OnClockSetRate(
            [In] long hnsSystemTime,
            [In] float flRate
        );
    }
}