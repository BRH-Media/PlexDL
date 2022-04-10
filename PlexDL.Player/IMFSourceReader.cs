using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
    internal interface IMFSourceReader
    {
        [PreserveSig]
        HResult GetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfSelected
        );

        [PreserveSig]
        HResult SetStreamSelection(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] bool fSelected
        );

        [PreserveSig]
        HResult GetNativeMediaType(
            int dwStreamIndex,
            int dwMediaTypeIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetCurrentMediaType(
            int dwStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult SetCurrentMediaType(
            int dwStreamIndex,
            [In, Out] MFInt pdwReserved,
            IMFMediaType pMediaType
        );

        [PreserveSig]
        HResult SetCurrentPosition(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidTimeFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] ConstPropVariant varPosition
        );

        [PreserveSig]
        HResult ReadSample(
            int dwStreamIndex,
            MF_SOURCE_READER_CONTROL_FLAG dwControlFlags,
            out int pdwActualStreamIndex,
            out MF_SOURCE_READER_FLAG pdwStreamFlags,
            out long pllTimestamp,
            out IMFSample ppSample
        );

        [PreserveSig]
        HResult Flush(
            int dwStreamIndex
        );

        [PreserveSig]
        HResult GetServiceForStream(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
        );

        [PreserveSig]
        HResult GetPresentationAttribute(
            int dwStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidAttribute,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalCookie = "IMFSourceReader.GetPresentationAttribute", MarshalTypeRef = typeof(PVMarshaler))] PropVariant pvarAttribute
        );
    }
}