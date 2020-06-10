using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFBYTESTREAM_* defines")]
    internal enum MFByteStreamCapabilities
    {
        None = 0x00000000,
        IsReadable = 0x00000001,
        IsWritable = 0x00000002,
        IsSeekable = 0x00000004,
        IsRemote = 0x00000008,
        IsDirectory = 0x00000080,
        HasSlowSeek = 0x00000100,
        IsPartiallyDownloaded = 0x00000200,
        ShareWrite = 0x00000400,
        DoesNotUseNetwork = 0x00000800,
    }
}