using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("2EB1E945-18B8-4139-9B1A-D5D584818530")]
    internal interface IMFClock
    {
        [PreserveSig]
        HResult GetClockCharacteristics(
            out MFClockCharacteristicsFlags pdwCharacteristics
        );

        [PreserveSig]
        HResult GetCorrelatedTime(
            [In] int dwReserved,
            out long pllClockTime,
            out long phnsSystemTime
        );

        [PreserveSig]
        HResult GetContinuityKey(
            out int pdwContinuityKey
        );

        [PreserveSig]
        HResult GetState(
            [In] int dwReserved,
            out MFClockState peClockState
        );

        //[PreserveSig]
        //HResult GetProperties(
        //    out MFClockProperties pClockProperties
        //    );
        [PreserveSig]
        HResult GetProperties();
    }
}