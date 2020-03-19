using System;

namespace PlexDL.Player
{
    [Flags]
    [UnmanagedName("MFVideoAspectRatioMode")]
    internal enum MFVideoAspectRatioMode
    {
        None = 0x00000000,
        PreservePicture = 0x00000001,
        PreservePixel = 0x00000002,
        NonLinearStretch = 0x00000004,
        Mask = 0x00000007
    }
}