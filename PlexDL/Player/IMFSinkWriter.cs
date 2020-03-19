using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport]
    [System.Security.SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
    internal interface IMFSinkWriter
    {
        [PreserveSig]
        HResult AddStream(
            IMFMediaType pTargetMediaType,
            out int pdwStreamIndex
        );

        [PreserveSig]
        HResult SetInputMediaType(
            int dwStreamIndex,
            IMFMediaType pInputMediaType,
            IMFAttributes pEncodingParameters
        );

        [PreserveSig]
        HResult BeginWriting();

        [PreserveSig]
        HResult WriteSample(
            int dwStreamIndex,
            IMFSample pSample
        );

        [PreserveSig]
        HResult SendStreamTick(
            int dwStreamIndex,
            long llTimestamp
        );

        [PreserveSig]
        HResult PlaceMarker(
            int dwStreamIndex,
            IntPtr pvContext
        );

        [PreserveSig]
        HResult NotifyEndOfSegment(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult Flush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult Finalize_();

        [PreserveSig]
        HResult GetServiceForStream(
            int dwStreamIndex,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid guidService,
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );

        [PreserveSig]
        HResult GetStatistics(
            int dwStreamIndex,
            out MF_SINK_WRITER_STATISTICS pStats
        );
    }
}