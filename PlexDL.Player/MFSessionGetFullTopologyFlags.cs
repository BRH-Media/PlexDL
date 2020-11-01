using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFSESSION_GETFULLTOPOLOGY_FLAGS")]
    internal enum MFSessionGetFullTopologyFlags
    {
        None = 0x0,
        Current = 0x1
    }
}