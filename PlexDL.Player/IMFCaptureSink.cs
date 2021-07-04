using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("72d6135b-35e9-412c-b926-fd5265f2a885"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureSink
    {
        [PreserveSig]
        HResult GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
        );

        [PreserveSig]
        HResult AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
        );

        [PreserveSig]
        HResult Prepare();

        [PreserveSig]
        HResult RemoveAllStreams();
    }
}