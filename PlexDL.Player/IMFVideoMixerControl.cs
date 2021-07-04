using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("A5C6C53F-C202-4AA5-9695-175BA8C508A5")]
    internal interface IMFVideoMixerControl
    {
        [PreserveSig]
        HResult SetStreamZOrder(
            [In] int dwStreamID,
            [In] int dwZ
        );

        [PreserveSig]
        HResult GetStreamZOrder(
            [In] int dwStreamID,
            out int pdwZ
        );

        [PreserveSig]
        HResult SetStreamOutputRect(
            [In] int dwStreamID,
            [In] MFVideoNormalizedRect pnrcOutput
        );

        [PreserveSig]
        HResult GetStreamOutputRect(
            [In] int dwStreamID,
            [Out, MarshalAs(UnmanagedType.LPStruct)] MFVideoNormalizedRect pnrcOutput
        );
    }
}