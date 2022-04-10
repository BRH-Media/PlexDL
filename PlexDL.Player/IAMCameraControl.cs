using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("C6E13370-30AC-11d0-A18C-00A0C9118956")]
    internal interface IAMCameraControl
    {
        [PreserveSig]
        HResult GetRange(
            [In] CameraControlProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out CameraControlFlags pCapsFlags
        );

        [PreserveSig]
        HResult Set(
            [In] CameraControlProperty Property,
            [In] int lValue,
            [In] CameraControlFlags Flags
        );

        [PreserveSig]
        HResult Get(
            [In] CameraControlProperty Property,
            [Out] out int lValue,
            [Out] out CameraControlFlags Flags
        );
    }
}