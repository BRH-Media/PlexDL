using System;

namespace PlexDL.Player
{
    [Flags]
    [UnmanagedName("MFMEDIASOURCE_CHARACTERISTICS")]
    internal enum MFMediaSourceCharacteristics
    {
        None = 0,
        IsLive = 0x1,
        CanSeek = 0x2,
        CanPause = 0x4,
        HasSlowSeek = 0x8,
        HasMultiplePresentations = 0x10,
        CanSkipForward = 0x20,
        CanSkipBackward = 0x40,
        DoesNotUseNetwork = 0x80
    }
}