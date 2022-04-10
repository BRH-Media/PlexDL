using System;

namespace PlexDL.Player
{
    internal struct VideoStream
    {
        internal Guid       MediaType;
        internal int        StreamIndex;
        internal bool       Selected;
        internal string     Name;
        internal string     Language;
        internal int        BitRate;
        internal float      FrameRate;

        internal int        SourceWidth;
        internal int        SourceHeight;
        internal int        SourceRotation;

        internal bool       PixelAspectRatio;
        internal double     PixelWidthRatio;
        internal double     PixelHeightRatio;

        internal bool       Rotated;
        internal int        Rotation;

        // used to show one image when video has 2 images beside each other
        //internal int    View3D; // 0 = none; 2 = left/right, 3 = top/bottom
    }
}