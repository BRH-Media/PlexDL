using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("90377834-21D0-4DEE-8214-BA2E3E6C1127"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFMediaSession : IMFMediaEventGenerator
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

        #endregion IMFMediaEventGenerator methods

        [PreserveSig]
        HResult SetTopology(
            [In] MFSessionSetTopologyFlags dwSetTopologyFlags,
            [In, MarshalAs(UnmanagedType.Interface)] IMFTopology pTopology
        );

        [PreserveSig]
        HResult ClearTopologies();

        [PreserveSig]
        HResult Start(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant pvarStartPosition
        );

        [PreserveSig]
        HResult Pause();

        [PreserveSig]
        HResult Stop();

        [PreserveSig]
        HResult Close();

        [PreserveSig]
        HResult Shutdown();

        [PreserveSig]
        HResult GetClock(
            [MarshalAs(UnmanagedType.Interface)] out IMFClock ppClock
        );

        [PreserveSig]
        HResult GetSessionCapabilities(
            out MFSessionCapabilities pdwCaps
        );

        [PreserveSig]
        HResult GetFullTopology(
            [In] MFSessionGetFullTopologyFlags dwGetFullTopologyFlags,
            [In] long TopoId,
            [MarshalAs(UnmanagedType.Interface)] out IMFTopology ppFullTopology
        );
    }
}