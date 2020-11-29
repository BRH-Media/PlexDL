using PlexDL.Common.Globals.Providers;
using System.Xml;

namespace PlexDL.Common.API.PlexAPI.Metadata.Handlers.Parsers
{
    public static class XmlMetadataIntegers
    {
        public static int GetParentIndex(XmlDocument metadata)
        {
            const string attr = "parentIndex";
            var contentType = ObjectProvider.CurrentContentType;
            return int.TryParse(XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr), out var r)
                ? r
                : -1;
        }

        public static int GetIndex(XmlDocument metadata)
        {
            const string attr = "index";
            var contentType = ObjectProvider.CurrentContentType;
            return int.TryParse(XmlMetadataHelpers.GetContentAttribute(metadata, contentType, attr), out var r)
                ? r
                : -1;
        }
    }
}