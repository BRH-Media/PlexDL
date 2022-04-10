using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
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