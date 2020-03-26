using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("C6E13360-30AC-11D0-A18C-00A0C9118956")]
    internal interface IAMVideoProcAmp
    {
        [PreserveSig]
        HResult GetRange(
            [In] VideoProcAmpProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out VideoProcAmpFlags pCapsFlags
        );

        [PreserveSig]
        HResult Set(
            [In] VideoProcAmpProperty Property,
            [In] int lValue,
            [In] VideoProcAmpFlags Flags
        );

        [PreserveSig]
        HResult Get(
            [In] VideoProcAmpProperty Property,
            [Out] out int lValue,
            [Out] out VideoProcAmpFlags Flags
        );
    }
}