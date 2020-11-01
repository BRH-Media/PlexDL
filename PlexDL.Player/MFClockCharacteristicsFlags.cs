using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFCLOCK_CHARACTERISTICS_FLAGS")]
    internal enum MFClockCharacteristicsFlags
    {
        None = 0,
        Frequency10Mhz = 0x2,
        AlwaysRunning = 0x4,
        IsSystemClock = 0x8
    }
}