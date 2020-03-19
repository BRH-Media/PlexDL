using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("279A808D-AEC7-40C8-9C6B-A6B492C78A66")]
    internal interface IMFMediaSource : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator methods

        [PreserveSig]
        new HResult GetEvent(
            [In] MFEventFlag dwFlags,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        new HResult BeginGetEvent(
            [In] [MarshalAs(UnmanagedType.Interface)]
            IMFAsyncCallback pCallback,
            [In] [MarshalAs(UnmanagedType.IUnknown)]
            object o
        );

        [PreserveSig]
        new HResult EndGetEvent(
            IMFAsyncResult pResult,
            out IMFMediaEvent ppEvent
        );

        [PreserveSig]
        new HResult QueueEvent(
            [In] MediaEventType met,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidExtendedType,
            [In] HResult hrStatus,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant pvValue
        );

        #endregion IMFMediaEventGenerator methods

        [PreserveSig]
        HResult GetCharacteristics(
            out MFMediaSourceCharacteristics pdwCharacteristics
        );

        [PreserveSig]
        HResult CreatePresentationDescriptor(
            out IMFPresentationDescriptor ppPresentationDescriptor
        );

        [PreserveSig]
        HResult Start(
            [In] [MarshalAs(UnmanagedType.Interface)]
            IMFPresentationDescriptor pPresentationDescriptor,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid pguidTimeFormat,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            ConstPropVariant pvarStartPosition
        );

        [PreserveSig]
        HResult Stop();

        [PreserveSig]
        HResult Pause();

        [PreserveSig]
        HResult Shutdown();
    }
}