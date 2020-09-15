using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMediaPart
    {
        private string containerField;

        private uint durationField;

        private string fileField;

        private ushort idField;

        private string keyField;

        private ulong sizeField;
        private MediaContainerVideoMediaPartStream[] streamField;

        private string videoProfileField;

        /// <remarks />
        [XmlElement("Stream")]
        public MediaContainerVideoMediaPartStream[] Stream
        {
            get => streamField;
            set => streamField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string key
        {
            get => keyField;
            set => keyField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint duration
        {
            get => durationField;
            set => durationField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string file
        {
            get => fileField;
            set => fileField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ulong size
        {
            get => sizeField;
            set => sizeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string container
        {
            get => containerField;
            set => containerField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string videoProfile
        {
            get => videoProfileField;
            set => videoProfileField = value;
        }
    }
}