using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFNetAuthenticationFlags")]
    internal enum MFNetAuthenticationFlags
    {
        None = 0,
        Proxy = 0x00000001,
        ClearText = 0x00000002,
        LoggedOnUser = 0x00000004
    }
}