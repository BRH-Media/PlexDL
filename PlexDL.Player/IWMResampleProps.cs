using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("E7E9984F-F09F-4DA4-903F-6E2E0EFE56B5")]
    internal interface IWMResampleProps
    {
        [PreserveSig]
        HResult SetHalfFilterLength(
            int lhalfFilterLen
        );

        [PreserveSig]
        HResult SetUserChannelMtx(
            float[] userChannelMtx
        );
        // todo marsshal float ?
    }
}