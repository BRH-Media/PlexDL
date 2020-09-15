// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class MediaContainer
    {
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

        private byte sizeField;
        private MediaContainerVideo videoField;

        /// <remarks />
        public MediaContainerVideo Video
        {
            get => videoField;
            set => videoField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte size
        {
            get => sizeField;
            set => sizeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string identifier
        {
            get => identifierField;
            set => identifierField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string mediaTagPrefix
        {
            get => mediaTagPrefixField;
            set => mediaTagPrefixField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint mediaTagVersion
        {
            get => mediaTagVersionField;
            set => mediaTagVersionField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string playQueueID
        {
            get => playQueueIDField;
            set => playQueueIDField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint playQueueSelectedItemID
        {
            get => playQueueSelectedItemIDField;
            set => playQueueSelectedItemIDField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte playQueueSelectedItemOffset
        {
            get => playQueueSelectedItemOffsetField;
            set => playQueueSelectedItemOffsetField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort playQueueSelectedMetadataItemID
        {
            get => playQueueSelectedMetadataItemIDField;
            set => playQueueSelectedMetadataItemIDField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte playQueueShuffled
        {
            get => playQueueShuffledField;
            set => playQueueShuffledField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string playQueueSourceURI
        {
            get => playQueueSourceURIField;
            set => playQueueSourceURIField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte playQueueTotalCount
        {
            get => playQueueTotalCountField;
            set => playQueueTotalCountField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte playQueueVersion
        {
            get => playQueueVersionField;
            set => playQueueVersionField = value;
        }
    }
}