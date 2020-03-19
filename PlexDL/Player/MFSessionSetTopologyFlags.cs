using System;

namespace PlexDL.Player
{
    [Flags]
    [UnmanagedName("MFSESSION_SETTOPOLOGY_FLAGS")]
    internal enum MFSessionSetTopologyFlags
    {
        None = 0x0,
        Immediate = 0x1,
        NoResolution = 0x2,
        ClearCurrent = 0x4
    }
}