using System;
using System.Xml.Serialization;

namespace PlexDL.MyPlex
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GetServersResponse : PlexRest
    {
        private Container[] _itemsField;

        [XmlElement("MediaContainer")]
        public Container[] Items
        {
            get => _itemsField;
            set => _itemsField = value;
        }
    }
}