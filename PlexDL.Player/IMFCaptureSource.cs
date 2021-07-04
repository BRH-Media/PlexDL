using System;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("439a42a8-0d2c-4505-be83-f79b2a05d5c4"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureSource
    {
        [PreserveSig]
        HResult GetCaptureDeviceSource(
            MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType,
            out IMFMediaSource ppMediaSource
        );

        [PreserveSig]
        HResult GetCaptureDeviceActivate(
            MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType,
            out IMFActivate ppActivate
        );

        [PreserveSig]
        HResult GetService(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
        );

        [PreserveSig]
        HResult AddEffect(
            int dwSourceStreamIndex,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown
        );

        [PreserveSig]
        HResult RemoveEffect(
            int dwSourceStreamIndex,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown
        );

        [PreserveSig]
        HResult RemoveAllEffects(
            int dwSourceStreamIndex
        );

        [PreserveSig]
        HResult GetAvailableDeviceMediaType(
            int dwSourceStreamIndex,
            int dwMediaTypeIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult SetCurrentDeviceMediaType(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType
        );

        [PreserveSig]
        HResult GetCurrentDeviceMediaType(
            int dwSourceStreamIndex,
            out IMFMediaType ppMediaType
        );

        [PreserveSig]
        HResult GetDeviceStreamCount(
            out int pdwStreamCount
        );

        [PreserveSig]
        HResult GetDeviceStreamCategory(
            int dwSourceStreamIndex,
            out MF_CAPTURE_ENGINE_STREAM_CATEGORY pStreamCategory
        );

        [PreserveSig]
        HResult GetMirrorState(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfMirrorState
        );

        [PreserveSig]
        HResult SetMirrorState(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] bool fMirrorState
        );

        [PreserveSig]
        HResult GetStreamIndexFromFriendlyName(
            int uifriendlyName,
            out int pdwActualStreamIndex
        );
    }
}