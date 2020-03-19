using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6AB0000C-FECE-4d1f-A2AC-A9573530656E")]
    internal interface IMFVideoProcessor
    {
        [PreserveSig]
        HResult GetAvailableVideoProcessorModes(
            out int lpdwNumProcessingModes,
            //[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out Guid[] ppVideoProcessingModes);
            [MarshalAs(UnmanagedType.LPArray)] out Guid[] ppVideoProcessingModes);

        [PreserveSig]
        HResult GetVideoProcessorCaps(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid lpVideoProcessorMode,
            out DXVA2VideoProcessorCaps lpVideoProcessorCaps);

        [PreserveSig]
        HResult GetVideoProcessorMode(
            out Guid lpMode);

        [PreserveSig]
        HResult SetVideoProcessorMode(
            [In] [MarshalAs(UnmanagedType.LPStruct)]
            Guid lpMode);

        [PreserveSig]
        HResult GetProcAmpRange(
            DXVA2ProcAmp dwProperty,
            out DXVA2ValueRange pPropRange);

        [PreserveSig]
        HResult GetProcAmpValues(
            DXVA2ProcAmp dwFlags,
            [Out] [MarshalAs(UnmanagedType.LPStruct)]
            DXVA2ProcAmpValues Values);

        [PreserveSig]
        HResult SetProcAmpValues(
            DXVA2ProcAmp dwFlags,
            [In] DXVA2ProcAmpValues pValues);

        //[PreserveSig]
        //HResult GetFilteringRange(
        //    DXVA2Filters dwProperty,
        //    out DXVA2ValueRange pPropRange);
        [PreserveSig]
        HResult GetFilteringRange();

        //[PreserveSig]
        //HResult GetFilteringValue(
        //    DXVA2Filters dwProperty,
        //    out int pValue);
        [PreserveSig]
        HResult GetFilteringValue();

        //[PreserveSig]
        //HResult SetFilteringValue(
        //    DXVA2Filters dwProperty,
        //    [In] ref int pValue);
        [PreserveSig]
        HResult SetFilteringValue();

        [PreserveSig]
        HResult GetBackgroundColor(
            out int lpClrBkg);

        [PreserveSig]
        HResult SetBackgroundColor(
            int ClrBkg);
    }
}