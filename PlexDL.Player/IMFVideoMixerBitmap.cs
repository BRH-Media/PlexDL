using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("814C7B20-0FDB-4eec-AF8F-F957C8F69EDC")]
    internal interface IMFVideoMixerBitmap
    {
        [PreserveSig]
        HResult SetAlphaBitmap(
            [In, MarshalAs(UnmanagedType.LPStruct)] MFVideoAlphaBitmap pBmpParms);

        [PreserveSig]
        HResult ClearAlphaBitmap();

        [PreserveSig]
        HResult UpdateAlphaBitmapParameters(
            [In] MFVideoAlphaBitmapParams pBmpParms);

        [PreserveSig]
        HResult GetAlphaBitmapParameters(
            [Out] MFVideoAlphaBitmapParams pBmpParms);
    }
}