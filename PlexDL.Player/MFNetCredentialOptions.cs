using System;

namespace PlexDL.Player
{
    [Flags, UnmanagedName("MFNetCredentialOptions")]
    internal enum MFNetCredentialOptions
    {
        None = 0,
        Save = 0x00000001,
        DontCache = 0x00000002,
        AllowClearText = 0x00000004,
    }
}