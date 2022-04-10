using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("0A97B3CF-8E7C-4A3D-8F8C-0C843DC247FB"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFStreamSink : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator methods

        [PreserveSig]
        new HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        new HResult BeginGetEvent(
            [In, MarshalAs(UnmanagedType.Interface)] IMFAsyncCallback pCallback,
            [In, MarshalAs(UnmanagedType.IUnknown)] object o
        );

        [PreserveSig]
        new HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        new HResult QueueEvent(
            [In] MediaEventType met,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidExtendedType,
            [In] HResult hrStatus,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvValue
        );

        #endregion

        [PreserveSig]
        HResult GetMediaSink(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaSink ppMediaSink
        );

        [PreserveSig]
        HResult GetIdentifier(
            out int pdwIdentifier
        );

        [PreserveSig]
        HResult GetMediaTypeHandler(
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaTypeHandler ppHandler
        );

        [PreserveSig]
        HResult ProcessSample(
            [In, MarshalAs(UnmanagedType.Interface)] IMFSample pSample
        );

        [PreserveSig]
        HResult PlaceMarker(
            [In] MFStreamSinkMarkerType eMarkerType,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarMarkerValue,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarContextValue
        );

        [PreserveSig]
        HResult Flush();
    }
}