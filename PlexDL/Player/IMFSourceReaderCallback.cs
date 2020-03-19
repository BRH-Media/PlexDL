using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport]
    [System.Security.SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("deec8d99-fa1d-4d82-84c2-2c8969944867")]
    internal interface IMFSourceReaderCallback
    {
        [PreserveSig]
        HResult OnReadSample(
            HResult hrStatus,
            int dwStreamIndex,
            MF_SOURCE_READER_FLAG dwStreamFlags,
            long llTimestamp,
            IMFSample pSample
        );

        [PreserveSig]
        HResult OnFlush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult OnEvent(
            int dwStreamIndex,
            IMFMediaEvent pEvent
        );
    }
}