using PlexDL.Common.Globals.Providers;
using System.Xml;

namespace PlexDL.Common.API.PlexAPI.Metadata.Handlers.Parsers
{
    public static class XmlMetadataStrings
    {
        public static string GetContentGenre(XmlDocument metadata)
        {
            const string table = "Genre";
            const string attr = "tag";
            return XmlMetadataHelpers.GetContentAttribute(metadata, table, attr);
        }

        public static string GetContentSynopsis(XmlDocument metadata)
        {
            const string attr = "summary";
            const string def = @"Plot synopsis not provided";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr, def);
        }

        public static string GetParentTitle(XmlDocument metadata)
        {
            const string attr = "parentTitle";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr);
        }

        public static string GetGrandparentTitle(XmlDocument metadata)
        {
            const string attr = "grandparentTitle";
            var contentType = ObjectProvider.CurrentContentType;
            return XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr);
        }
    }
}