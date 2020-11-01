using System.Runtime.InteropServices;
using System.Security;

namespace PlexDL.Player
{
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("88DDCD21-03C3-4275-91ED-55EE3929328F"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMFRateControl
    {
        [PreserveSig]
        HResult SetRate(
            [In, MarshalAs(UnmanagedType.Bool)] bool fThin,
            [In] float flRate
        );

        [PreserveSig]
        HResult GetRate(
            [In, Out, MarshalAs(UnmanagedType.Bool)] ref bool pfThin,
            out float pflRate
        );
    }
}