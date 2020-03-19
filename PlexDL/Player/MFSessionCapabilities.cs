using System;

namespace PlexDL.Player
{
    [Flags]
    [UnmanagedName("MFSESSIONCAP_* defines")]
    internal enum MFSessionCapabilities
    {
        None = 0x00000000,
        Start = 0x00000001,
        Seek = 0x00000002,
        Pause = 0x00000004,
        RateForward = 0x00000010,
        RateReverse = 0x00000020,
        DoesNotUseNetwork = 0x00000040
    }
}