using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("AD4C1B00-4BF7-422F-9175-756693D9130D")]
    internal interface IMFByteStream
    {
        [PreserveSig]
        HResult GetCapabilities(
            out MFByteStreamCapabilities pdwCapabilities
        );

        [PreserveSig]
        HResult GetLength(
            out long pqwLength
        );

        [PreserveSig]
        HResult SetLength(
            [In] long qwLength
        );

        [PreserveSig]
        HResult GetCurrentPosition(
            out long pqwPosition
        );

        [PreserveSig]
        HResult SetCurrentPosition(
            [In] long qwPosition
        );

        [PreserveSig]
        HResult IsEndOfStream(
            [MarshalAs(UnmanagedType.Bool)] out bool pfEndOfStream
        );

        [PreserveSig]
        HResult Read(
            IntPtr pb,
            [In] int cb,
            out int pcbRead
        );

        [PreserveSig]
        HResult BeginRead(
            IntPtr pb,
            [In] int cb,
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
        );

        [PreserveSig]
        HResult EndRead(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
            out int pcbRead
        );

        [PreserveSig]
        HResult Write(
            IntPtr pb,
            [In] int cb,
            out int pcbWritten
        );

        [PreserveSig]
        HResult BeginWrite(
            IntPtr pb,
            [In] int cb,
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkState
        );

        [PreserveSig]
        HResult EndWrite(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncResult pResult,
            out int pcbWritten
        );

        [PreserveSig]
        HResult Seek(
            [In] MFByteStreamSeekOrigin SeekOrigin,
            [In] long llSeekOffset,
            [In] MFByteStreamSeekingFlags dwSeekFlags,
            out long pqwCurrentPosition
        );

        [PreserveSig]
        HResult Flush();

        [PreserveSig]
        HResult Close();
    }
}