using System;

namespace PlexDL.Player
{
    internal struct AudioStream
    {
        internal Guid   MediaType;
        internal int    StreamIndex;
        internal bool   Selected;
        internal string Name;
        internal string Language;
        internal int    ChannelCount;
        internal int    ChannelCountRestore;
        internal int    SampleRate;
        internal int    BitDepth;
        internal int    BitRate;

        internal AudioDevice            AudioDevice;
        internal IMFAudioStreamVolume   MF_VolumeService;
        internal float                  Volume;
        internal float                  Balance;
        internal float[]                ChannelVolumes;
        internal bool                   Enabled;
    }
}