using PlexDL.Common.Pxz.Structures;
using System.Xml;

namespace PlexDL.Common.Pxz
{
    public static class Extensions
    {
        public static XmlDocument ToXml(this PxzIndex obj)
        {
            return Serializers.PxzIndexToXml(obj);
        }
    }
}