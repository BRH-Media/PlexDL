using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("7FF12CCE-F76F-41C2-863B-1666C8E5E139"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFPresentationTimeSource : IMFClock
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
        HResult GetUnderlyingClock(
            [MarshalAs(UnmanagedType.Interface)] out IMFClock ppClock
        );
    }
}