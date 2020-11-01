using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFASYNC_* defines")]
    internal enum MFASync
    {
        None = 0,
        FastIOProcessingCallback = 0x00000001,
        SignalCallback = 0x00000002,
        BlockingCallback = 0x00000004,
        ReplyCallback = 0x00000008,
        LocalizeRemoteCallback = 0x00000010,
    }
}