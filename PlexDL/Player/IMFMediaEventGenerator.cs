using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaEventGenerator
    {
        [PreserveSig]
        HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        HResult BeginGetEvent(
            [In] [MarshalAs(UnmanagedType.Interface)]
            IMFAsyncCallback pCallback,
            [In] [MarshalAs(UnmanagedType.IUnknown)]
            object o
        );

        [PreserveSig]
        HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        HResult QueueEvent(
            [In] MediaEventType met,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidExtendedType,
            [In] HResult hrStatus,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant pvValue
        );
    }
}