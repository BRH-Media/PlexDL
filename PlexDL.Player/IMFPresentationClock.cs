using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("868CE85C-8EA9-4F55-AB82-B009A910A805")]
    internal interface IMFPresentationClock : IMFClock
    {
        #region IMFClock methods

        [PreserveSig]
        new HResult GetClockCharacteristics(
            out MFClockCharacteristicsFlags pdwCharacteristics
        );

        [PreserveSig]
        new HResult GetCorrelatedTime(
            [In] int dwReserved,
            out long pllClockTime,
            out long phnsSystemTime
        );

        [PreserveSig]
        new HResult GetContinuityKey(
            out int pdwContinuityKey
        );

        [PreserveSig]
        new HResult GetState(
            [In] int dwReserved,
            out MFClockState peClockState
        );

        [PreserveSig]
#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
        new HResult GetProperties(
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
            out MFClockProperties pClockProperties
        );

        #endregion

        [PreserveSig]
        HResult SetTimeSource(
            [In, MarshalAs(UnmanagedType.Interface)] IMFPresentationTimeSource pTimeSource
        );

        [PreserveSig]
        HResult GetTimeSource(
            [MarshalAs(UnmanagedType.Interface)] out IMFPresentationTimeSource ppTimeSource
        );

        [PreserveSig]
        HResult GetTime(
            out long phnsClockTime
        );

        [PreserveSig]
        HResult AddClockStateSink(
            [In, MarshalAs(UnmanagedType.Interface)] IMFClockStateSink pStateSink
        );

        [PreserveSig]
        HResult RemoveClockStateSink(
            [In, MarshalAs(UnmanagedType.Interface)] IMFClockStateSink pStateSink
        );

        [PreserveSig]
        HResult Start(
            [In] long llClockStartOffset
        );

        [PreserveSig]
        HResult Stop();

        [PreserveSig]
        HResult Pause();
    }
}