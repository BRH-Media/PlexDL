using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("56181D2D-E221-4ADB-B1C8-3CEE6A53F76F")]
    internal interface IMFMetadataProvider
    {
        [PreserveSig]
        HResult GetMFMetadata(
            [In, MarshalAs(UnmanagedType.Interface)]
            IMFPresentationDescriptor pPresentationDescriptor,
            [In] int dwStreamIdentifier,
            [In] int dwFlags, // must be zero
            [MarshalAs(UnmanagedType.Interface)] out IMFMetadata ppMFMetadata
        );
    }
}