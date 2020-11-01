using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("E93DCF6C-4B07-4E1E-8123-AA16ED6EADF5"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaTypeHandler
    {
        [PreserveSig]
        HResult IsMediaTypeSupported(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pMediaType,
            IntPtr ppMediaType  //[MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetMediaTypeCount(
            out int pdwTypeCount
        );

        [PreserveSig]
        HResult GetMediaTypeByIndex(
            [In] int dwIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
        );

        [PreserveSig]
        HResult SetCurrentMediaType(
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pMediaType
        );

        [PreserveSig]
        HResult GetCurrentMediaType(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetMajorType(
            out Guid pguidMajorType
        );
    }
}