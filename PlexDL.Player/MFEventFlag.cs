using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MF_EVENT_FLAG_* defines")]
    internal enum MFEventFlag
    {
        None = 0,
        NoWait = 0x00000001
    }
}