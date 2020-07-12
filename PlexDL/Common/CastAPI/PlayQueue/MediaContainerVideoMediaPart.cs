using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMediaPart
    {
        private MediaContainerVideoMediaPartStream[] streamField;

        private ushort idField;

        private string keyField;

        private uint durationField;

        private string fileField;

        private ulong sizeField;

        private string containerField;

        private string videoProfileField;

        /// <remarks/>
        [XmlElement("Stream")]
        public MediaContainerVideoMediaPartStream[] Stream
        {
            get { return streamField; }
            set { streamField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string key
        {
            get { return keyField; }
            set { keyField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint duration
        {
            get { return durationField; }
            set { durationField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string file
        {
            get { return fileField; }
            set { fileField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ulong size
        {
            get { return sizeField; }
            set { sizeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string container
        {
            get { return containerField; }
            set { containerField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string videoProfile
        {
            get { return videoProfileField; }
            set { videoProfileField = value; }
        }
    }
}