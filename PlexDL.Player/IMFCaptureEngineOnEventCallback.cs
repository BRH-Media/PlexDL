using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("aeda51c0-9025-4983-9012-de597b88b089"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureEngineOnEventCallback
    {
        [PreserveSig]
        HResult OnEvent(
            IMFMediaEvent pEvent
        );
    }
}