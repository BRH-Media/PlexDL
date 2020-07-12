using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoDirector
    {
        private ushort idField;

        private string filterField;

        private string tagField;

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string filter
        {
            get { return filterField; }
            set { filterField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string tag
        {
            get { return tagField; }
            set { tagField = value; }
        }
    }
}