using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("6EF2A660-47C0-4666-B13D-CBB717F2FA2C"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaSink
    {
        [PreserveSig]
        HResult GetCharacteristics(
            out MFMediaSinkCharacteristics pdwCharacteristics
        );

        [PreserveSig]
        HResult AddStreamSink(
            [In] int dwStreamSinkIdentifier,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pMediaType,
            [MarshalAs(UnmanagedType.Interface)] out IMFStreamSink ppStreamSink
        );

        [PreserveSig]
        HResult RemoveStreamSink(
            [In] int dwStreamSinkIdentifier
        );

        [PreserveSig]
        HResult GetStreamSinkCount(
            out int pcStreamSinkCount
        );

        [PreserveSig]
        HResult GetStreamSinkByIndex(
            [In] int dwIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFStreamSink ppStreamSink
        );

        [PreserveSig]
        HResult GetStreamSinkById(
            [In] int dwStreamSinkIdentifier,
            [MarshalAs(UnmanagedType.Interface)] out IMFStreamSink ppStreamSink
        );

        [PreserveSig]
        HResult SetPresentationClock(
            [In, MarshalAs(UnmanagedType.Interface)] IMFPresentationClock pPresentationClock
        );

        [PreserveSig]
        HResult GetPresentationClock(
            [MarshalAs(UnmanagedType.Interface)] out IMFPresentationClock ppPresentationClock
        );

        [PreserveSig]
        HResult Shutdown();
    }
}