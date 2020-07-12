using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoRole
    {
        private uint idField;

        private string filterField;

        private string tagField;

        private string roleField;

        private string thumbField;

        /// <remarks/>
        [XmlAttribute]
        public uint id
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

        /// <remarks/>
        [XmlAttribute]
        public string role
        {
            get { return roleField; }
            set { roleField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string thumb
        {
            get { return thumbField; }
            set { thumbField = value; }
        }
    }
}