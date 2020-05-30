using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;

namespace PlexDL.Common.API.Objects.AttributeTables
{
    public static partial class AttributeGatherers
    {
        //just to make it easier :)
        private static string FormatFramerate(PlexObject streamingContent)
        {
            return !string.Equals(streamingContent.StreamResolution.Framerate, "Unknown")
                ? ResolutionStandards.FullFpsSuffix(streamingContent.StreamResolution.Framerate)
                : "Unknown";
        }
    }
}