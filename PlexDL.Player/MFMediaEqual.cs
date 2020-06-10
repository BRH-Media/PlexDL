using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MF_MEDIATYPE_EQUAL_* defines")]
    internal enum MFMediaEqual
    {
        None = 0,
        MajorTypes = 0x00000001,
        FormatTypes = 0x00000002,
        FormatData = 0x00000004,
        FormatUserData = 0x00000008
    }
}