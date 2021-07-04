using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MEDIASINK_ defines")]
    internal enum MFMediaSinkCharacteristics
    {
        None = 0,
        FixedStreams = 0x00000001,
        CannotMatchClock = 0x00000002,
        Rateless = 0x00000004,
        ClockRequired = 0x00000008,
        CanPreroll = 0x00000010,
        RequireReferenceMediaType = 0x00000020
    }
}