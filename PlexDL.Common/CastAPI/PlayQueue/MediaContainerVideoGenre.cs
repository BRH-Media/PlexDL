using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoGenre
    {
        private string filterField;
        private ushort idField;

        private string tagField;

        /// <remarks />
        [XmlAttribute]
        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string filter
        {
            get => filterField;
            set => filterField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string tag
        {
            get => tagField;
            set => tagField = value;
        }
    }
}