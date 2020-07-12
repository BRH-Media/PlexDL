// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class MediaContainer
    {
        private MediaContainerVideo videoField;

        private byte sizeField;

        private string identifierField;

        private string mediaTagPrefixField;

        private uint mediaTagVersionField;

        private string playQueueIDField;

        private uint playQueueSelectedItemIDField;

        private byte playQueueSelectedItemOffsetField;

        private ushort playQueueSelectedMetadataItemIDField;

        private byte playQueueShuffledField;

        private string playQueueSourceURIField;

        private byte playQueueTotalCountField;

        private byte playQueueVersionField;

        /// <remarks/>
        public MediaContainerVideo Video
        {
            get { return videoField; }
            set { videoField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte size
        {
            get { return sizeField; }
            set { sizeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string identifier
        {
            get { return identifierField; }
            set { identifierField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string mediaTagPrefix
        {
            get { return mediaTagPrefixField; }
            set { mediaTagPrefixField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint mediaTagVersion
        {
            get { return mediaTagVersionField; }
            set { mediaTagVersionField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string playQueueID
        {
            get { return playQueueIDField; }
            set { playQueueIDField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint playQueueSelectedItemID
        {
            get { return playQueueSelectedItemIDField; }
            set { playQueueSelectedItemIDField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte playQueueSelectedItemOffset
        {
            get { return playQueueSelectedItemOffsetField; }
            set { playQueueSelectedItemOffsetField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort playQueueSelectedMetadataItemID
        {
            get { return playQueueSelectedMetadataItemIDField; }
            set { playQueueSelectedMetadataItemIDField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte playQueueShuffled
        {
            get { return playQueueShuffledField; }
            set { playQueueShuffledField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string playQueueSourceURI
        {
            get { return playQueueSourceURIField; }
            set { playQueueSourceURIField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte playQueueTotalCount
        {
            get { return playQueueTotalCountField; }
            set { playQueueTotalCountField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte playQueueVersion
        {
            get { return playQueueVersionField; }
            set { playQueueVersionField = value; }
        }
    }
}