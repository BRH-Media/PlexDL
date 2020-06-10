using System;

namespace PlexDL.Player
{
    internal struct VideoStream
    {
        internal Guid MediaType;
        internal int StreamIndex;
        internal bool Selected;
        internal string Name;
        internal string Language;
        internal float FrameRate;
        internal int SourceWidth;
        internal int SourceHeight;
    }
}