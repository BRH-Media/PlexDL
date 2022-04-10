using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("d2d43cc8-48bb-4aa7-95db-10c06977e777"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCapturePhotoSink : IMFCaptureSink
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
        HResult SetOutputFileName(
            [MarshalAs(UnmanagedType.LPWStr)] string fileName
        );

        [PreserveSig]
        HResult SetSampleCallback(
            IMFCaptureEngineOnSampleCallback pCallback
        );

        [PreserveSig]
        HResult SetOutputByteStream(
            IMFByteStream pByteStream
        );
    }
}