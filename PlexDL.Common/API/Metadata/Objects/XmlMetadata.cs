using System.Xml;

namespace PlexDL.Common.API.Metadata.Objects
{
    public class XmlMetadata
    {
        public XmlDocument Xml { get; set; } = null;
        public string ApiUri { get; set; } = null;
    }
}