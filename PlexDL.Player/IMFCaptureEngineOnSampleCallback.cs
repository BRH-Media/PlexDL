using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("52150b82-ab39-4467-980f-e48bf0822ecd"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureEngineOnSampleCallback
    {
        [PreserveSig]
        HResult OnSample(
            IMFSample pSample
        );
    }
}