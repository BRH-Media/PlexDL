using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace PlexDL.Common.API.PlexAPI.Metadata.Handlers.Parsers
{
    public static class XmlMetadataObjects
    {
        public static List<PlexActor> GetActorsFromMetadata(XmlDocument metadata)
        {
            var actors = new List<PlexActor>();

            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var dtActors = sections.Tables["Role"];

            if (dtActors == null)
                return actors;

            foreach (DataRow r in dtActors.Rows)
            {
                var thumb = "";
                var role = "Unknown";
                var name = "Unknown";
                if (dtActors.Columns.Contains("thumb"))
                    if (r["thumb"] != null)
                        thumb = r["thumb"].ToString();
                if (dtActors.Columns.Contains("role"))
                    if (r["role"] != null)
                        role = r["role"].ToString();
                if (dtActors.Columns.Contains("tag"))
                    if (r["tag"] != null)
                        name = r["tag"].ToString();
                var a = new PlexActor
                {
                    ThumbnailUri = thumb,
                    ActorRole = role,
                    ActorName = name
                };
                actors.Add(a);
            }

            return actors;
        }

        public static Resolution GetContentResolution(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));

            const string def = "";
            var video = sections.Tables["Media"];

            var widthString = XmlMetadataHelpers.GetContentAttribute(video, "width", def);
            var heightString = XmlMetadataHelpers.GetContentAttribute(video, "height", def);
            var fpsString = XmlMetadataHelpers.GetContentAttribute(video, "videoFrameRate", def);

            var width = Methods.StringToInt(widthString);
            var height = Methods.StringToInt(heightString);
            var fpsStd = "";

            if (!Equals(fpsString, def))
                fpsStd = ResolutionStandards.FpsFromPlexStd(fpsString, false);

            var result = new Resolution
            {
                Width = width,
                Height = height,
                Framerate = fpsStd,
                VideoStandard = ResolutionStandards.StdFromHeight(height.ToString())
            };

            return result;
        }
    }
}