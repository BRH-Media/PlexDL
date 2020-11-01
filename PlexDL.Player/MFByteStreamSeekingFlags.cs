using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFBYTESTREAM_SEEK_FLAG_ defines")]
    internal enum MFByteStreamSeekingFlags
    {
        None = 0,
        CancelPendingIO = 1
    }
}