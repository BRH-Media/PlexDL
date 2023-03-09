using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlexDL.MyPlex
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(@"MediaContainer", Namespace = "", IsNullable = false)]
    public class GetServersResponse : PlexRest
    {
        [XmlElement(@"Server", IsNullable = true)]
        public List<Server> Servers { get; set; }
    }
}