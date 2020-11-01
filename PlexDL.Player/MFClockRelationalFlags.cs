using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFCLOCK_RELATIONAL_FLAGS")]
    internal enum MFClockRelationalFlags
    {
        None = 0,
        JitterNeverAhead = 0x1
    }
}