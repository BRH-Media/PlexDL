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
        internal int    Samplerate;
        internal int    Bitdepth;
        internal int    Bitrate;
    }
}