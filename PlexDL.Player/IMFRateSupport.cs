using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("0A9CCDBC-D797-4563-9667-94EC5D79292D"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFRateSupport
    {
        [PreserveSig]
        HResult GetSlowestRate(
            [In] MFRateDirection eDirection,
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            out float pflRate
        );

        [PreserveSig]
        HResult GetFastestRate(
            [In] MFRateDirection eDirection,
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            out float pflRate
        );

        [PreserveSig]
        HResult IsRateSupported(
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            [In] float flRate,
            [In, Out] MfFloat pflNearestSupportedRate
        );
    }
}