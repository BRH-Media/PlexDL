using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("77346cfd-5b49-4d73-ace0-5b52a859f2e0"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCapturePreviewSink : IMFCaptureSink
    {
        #region IMFCaptureSink Methods

        [PreserveSig]
        new HResult GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        new HResult GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
        );

        [PreserveSig]
        new HResult AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
        );

        [PreserveSig]
        new HResult Prepare();

        [PreserveSig]
        new HResult RemoveAllStreams();

        #endregion

        [PreserveSig]
        HResult SetRenderHandle(
            IntPtr handle
        );

        [PreserveSig]
        HResult SetRenderSurface(
            [MarshalAs(UnmanagedType.IUnknown)] object pSurface
        );

        [PreserveSig]
        HResult UpdateVideo(
            [In] MFVideoNormalizedRect pSrc,
            [In] MFRect pDst,
            [In] MFInt pBorderClr
        );

        [PreserveSig]
        HResult SetSampleCallback(
            int dwStreamSinkIndex,
            IMFCaptureEngineOnSampleCallback pCallback
        );

        [PreserveSig]
        HResult GetMirrorState(
            [MarshalAs(UnmanagedType.Bool)] out bool pfMirrorState
        );

        [PreserveSig]
        HResult SetMirrorState(
            [MarshalAs(UnmanagedType.Bool)] bool fMirrorState
        );

        [PreserveSig]
        HResult GetRotation(
            int dwStreamIndex,
            out int pdwRotationValue
        );

        [PreserveSig]
        HResult SetRotation(
            int dwStreamIndex,
            int dwRotationValue
        );

        [PreserveSig]
        HResult SetCustomSink(
            IMFMediaSink pMediaSink
        );
    }
}