using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("FBE5A32D-A497-4B61-BB85-97B1A848A6E3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFSourceResolver
    {
        [PreserveSig]
        HResult CreateObjectFromURL(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            [In] MFResolution dwFlags,
            IPropertyStore pProps,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
        );

        [PreserveSig]
        HResult CreateObjectFromByteStream(
            [In, MarshalAs(UnmanagedType.Interface)] IMFByteStream pByteStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            [In] MFResolution dwFlags,
            [In, MarshalAs(UnmanagedType.Interface)] IPropertyStore pProps,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
        );

        [PreserveSig]
        HResult BeginCreateObjectFromURL(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
            MFResolution dwFlags,
            IPropertyStore pProps,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppIUnknownCancelCookie,
            IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object punkState
        );

        [PreserveSig]
        HResult EndCreateObjectFromURL(
            IMFAsyncResult pResult,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.Interface)] out object ppObject
        );

        //[PreserveSig]
        //HResult BeginCreateObjectFromByteStream(
        //    [In, MarshalAs(UnmanagedType.Interface)] IMFByteStream pByteStream,
        //    [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
        //    [In] MFResolution dwFlags,
        //    IPropertyStore pProps,
        //    [MarshalAs(UnmanagedType.IUnknown)] out object ppIUnknownCancelCookie,
        //    IMFAsyncCallback pCallback,
        //    [MarshalAs(UnmanagedType.IUnknown)] object punkState
        //   );

        [PreserveSig]
        HResult EndCreateObjectFromByteStream(
            IMFAsyncResult pResult,
            out MFObjectType pObjectType,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppObject
        );

        [PreserveSig]
        HResult CancelObjectCreation(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pIUnknownCancelCookie
        );
    }
}