using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("3323b55a-f92a-4fe2-8edc-e9bfc0634d77"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureRecordSink : IMFCaptureSink
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
        HResult SetOutputByteStream(
            IMFByteStream pByteStream,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidContainerType
        );

        [PreserveSig]
        HResult SetOutputFileName(
            [MarshalAs(UnmanagedType.LPWStr)] string fileName
        );

        [PreserveSig]
        HResult SetSampleCallback(
            int dwStreamSinkIndex,
            IMFCaptureEngineOnSampleCallback pCallback
        );

        [PreserveSig]
        HResult SetCustomSink(
            IMFMediaSink pMediaSink
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
    }
}