using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("a6bba433-176b-48b2-b375-53aa03473207"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFCaptureEngine
    {
        [PreserveSig]
        HResult Initialize(
            IMFCaptureEngineOnEventCallback pEventCallback,
            IMFAttributes pAttributes,
            [MarshalAs(UnmanagedType.IUnknown)] object pAudioSource,
            [MarshalAs(UnmanagedType.IUnknown)] object pVideoSource
        );

        [PreserveSig]
        HResult StartPreview();

        [PreserveSig]
        HResult StopPreview();

        [PreserveSig]
        HResult StartRecord();

        [PreserveSig]
        HResult StopRecord(
            [MarshalAs(UnmanagedType.Bool)] bool bFinalize,
            [MarshalAs(UnmanagedType.Bool)] bool bFlushUnprocessedSamples
        );

        [PreserveSig]
        HResult TakePhoto();

        [PreserveSig]
        HResult GetSink(
            MF_CAPTURE_ENGINE_SINK_TYPE mfCaptureEngineSinkType,
            out IMFCaptureSink ppSink
        );

        [PreserveSig]
        HResult GetSource(
            out IMFCaptureSource ppSource
        );
    }
}