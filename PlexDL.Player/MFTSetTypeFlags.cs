using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("_MFT_SET_TYPE_FLAGS")]
    internal enum MFTSetTypeFlags
    {
        None = 0,
        TestOnly = 0x00000001
    }
}