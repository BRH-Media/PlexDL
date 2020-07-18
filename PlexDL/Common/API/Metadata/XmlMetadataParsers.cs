using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace PlexDL.Common.API.Metadata
{
    public static class XmlMetadataParsers
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

            var def = "";
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

        public static string GetContentGenre(XmlDocument metadata)
        {
            var table = "Genre";
            var attr = "tag";
            return XmlMetadataHelpers.GetContentAttribute(metadata, table, attr);
        }

        public static string GetContentSynopsis(XmlDocument metadata)
        {
            var attr = "summary";
            var def = @"Plot synopsis not provided";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr, def);
        }

        public static string GetParentTitle(XmlDocument metadata)
        {
            var attr = "parentTitle";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr);
        }

        public static string GetGrandparentTitle(XmlDocument metadata)
        {
            var attr = "grandparentTitle";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr);
        }
    }
}