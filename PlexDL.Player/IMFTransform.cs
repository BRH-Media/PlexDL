using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("BF94C121-5B05-4E6F-8000-BA598961414D")]
    internal interface IMFTransform
    {
        [PreserveSig]
        HResult GetStreamLimits(
            [Out] MFInt pdwInputMinimum,
            [Out] MFInt pdwInputMaximum,
            [Out] MFInt pdwOutputMinimum,
            [Out] MFInt pdwOutputMaximum
        );

        [PreserveSig]
        HResult GetStreamCount(
            [Out] MFInt pcInputStreams,
            [Out] MFInt pcOutputStreams
        );

        [PreserveSig]
        HResult GetStreamIDs(
            int dwInputIDArraySize,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] pdwInputIDs,
            int dwOutputIDArraySize,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] pdwOutputIDs
        );

        [PreserveSig]
        HResult GetInputStreamInfo(
            //int dwInputStreamID,
            //out MFTInputStreamInfo pStreamInfo
        );

        [PreserveSig]
        HResult GetOutputStreamInfo(
            //int dwOutputStreamID,
            //out MFTOutputStreamInfo pStreamInfo
        );

        [PreserveSig]
        HResult GetAttributes(
            [MarshalAs(UnmanagedType.Interface)] out IMFAttributes pAttributes
        );

        [PreserveSig]
        HResult GetInputStreamAttributes(
            int dwInputStreamID,
            [MarshalAs(UnmanagedType.Interface)] out IMFAttributes pAttributes
        );

        [PreserveSig]
        HResult GetOutputStreamAttributes(
            int dwOutputStreamID,
            [MarshalAs(UnmanagedType.Interface)] out IMFAttributes pAttributes
        );

        [PreserveSig]
        HResult DeleteInputStream(
            int dwStreamID
        );

        [PreserveSig]
        HResult AddInputStreams(
            int cStreams,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] adwStreamIDs
        );

        [PreserveSig]
        HResult GetInputAvailableType(
            int dwInputStreamID,
            int dwTypeIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
        );

        [PreserveSig]
        HResult GetOutputAvailableType(
            int dwOutputStreamID,
            int dwTypeIndex,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
        );

        [PreserveSig]
        HResult SetInputType(
            int dwInputStreamID,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pType,
            MFTSetTypeFlags dwFlags
        );

        [PreserveSig]
        HResult SetOutputType(
            int dwOutputStreamID,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaType pType,
            MFTSetTypeFlags dwFlags
        );

        [PreserveSig]
        HResult GetInputCurrentType(
            int dwInputStreamID,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
        );

        [PreserveSig]
        HResult GetOutputCurrentType(
            int dwOutputStreamID,
            [MarshalAs(UnmanagedType.Interface)] out IMFMediaType ppType
        );

        [PreserveSig]
        HResult GetInputStatus(
            //int dwInputStreamID,
            //out MFTInputStatusFlags pdwFlags
        );

        [PreserveSig]
        HResult GetOutputStatus(
            //out MFTOutputStatusFlags pdwFlags
        );

        [PreserveSig]
        HResult SetOutputBounds(
            long hnsLowerBound,
            long hnsUpperBound
        );

        [PreserveSig]
        HResult ProcessEvent(
            int dwInputStreamID,
            [In, MarshalAs(UnmanagedType.Interface)] IMFMediaEvent pEvent
        );

        [PreserveSig]
        HResult ProcessMessage(
            //MFTMessageType eMessage,
            //IntPtr ulParam
        );

        [PreserveSig]
        HResult ProcessInput(
            int dwInputStreamID,
            [MarshalAs(UnmanagedType.Interface)] IMFSample pSample,
            int dwFlags // Must be zero
        );

        [PreserveSig]
        HResult ProcessOutput(
            //MFTProcessOutputFlags dwFlags,
            //int cOutputBufferCount,
            //[In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] MFTOutputDataBuffer[] pOutputSamples,
            //out ProcessOutputStatus pdwStatus
        );
    }
}