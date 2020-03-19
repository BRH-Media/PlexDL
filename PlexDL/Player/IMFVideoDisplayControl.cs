using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [Guid("A490B1E4-AB84-4D31-A1B2-181E03B1077A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFVideoDisplayControl
    {
        [PreserveSig]
        HResult GetNativeVideoSize(
            [Out] MFSize pszVideo,
            [Out] MFSize pszARVideo
        );

        [PreserveSig]
        HResult GetIdealVideoSize(
            [Out] MFSize pszMin,
            [Out] MFSize pszMax
        );

        [PreserveSig]
        HResult SetVideoPosition(
            [In] MFVideoNormalizedRect pnrcSource,
            [In] MFRect prcDest
        );

        [PreserveSig]
        HResult GetVideoPosition(
            [Out] MFVideoNormalizedRect pnrcSource,
            [Out] MFRect prcDest
        );

        [PreserveSig]
        HResult SetAspectRatioMode(
            [In] MFVideoAspectRatioMode dwAspectRatioMode
        );

        [PreserveSig]
        HResult GetAspectRatioMode(
            out MFVideoAspectRatioMode pdwAspectRatioMode
        );

        [PreserveSig]
        HResult SetVideoWindow(
            [In] IntPtr hwndVideo
        );

        [PreserveSig]
        HResult GetVideoWindow(
            out IntPtr phwndVideo
        );

        [PreserveSig]
        HResult RepaintVideo();

        //[PreserveSig]
        //HResult GetCurrentImage(
        //    [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFVideoDisplayControl.GetCurrentImage", MarshalTypeRef = typeof(BMMarshaler))] BitmapInfoHeader pBih,
        //    out IntPtr pDib,
        //    out int pcbDib,
        //    out long pTimeStamp
        //    );
        [PreserveSig]
        HResult NotImpl();

        [PreserveSig]
        HResult SetBorderColor(
            [In] int Clr
        );

        [PreserveSig]
        HResult GetBorderColor(
            out int pClr
        );

        [PreserveSig]
        HResult SetRenderingPrefs(
            [In] MFVideoRenderPrefs dwRenderFlags
        );

        [PreserveSig]
        HResult GetRenderingPrefs(
            out MFVideoRenderPrefs pdwRenderFlags
        );

        [PreserveSig]
        HResult SetFullscreen(
            [In] [MarshalAs(UnmanagedType.Bool)] bool fFullscreen
        );

        [PreserveSig]
        HResult GetFullscreen(
            [MarshalAs(UnmanagedType.Bool)] out bool pfFullscreen
        );
    }
}