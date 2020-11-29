using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.API.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoWriter
    {
        private string filterField;
        private uint idField;

        private string tagField;

        /// <remarks />
        [XmlAttribute]
        public uint id
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