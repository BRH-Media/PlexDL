// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
namespace PlexDL.Common.API.CastAPI.PlayQueue
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MediaContainer
    {
        private MediaContainerVideo videoField;

        private byte sizeField;

        private string identifierField;

        private string mediaTagPrefixField;

        private uint mediaTagVersionField;

        private ushort playQueueIDField;

        private ushort playQueueSelectedItemIDField;

        private byte playQueueSelectedItemOffsetField;

        private ushort playQueueSelectedMetadataItemIDField;

        private byte playQueueShuffledField;

        private string playQueueSourceURIField;

        private byte playQueueTotalCountField;

        private byte playQueueVersionField;

        /// <remarks/>
        public MediaContainerVideo Video
        {
            get
            {
                return this.videoField;
            }
            set
            {
                this.videoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string mediaTagPrefix
        {
            get
            {
                return this.mediaTagPrefixField;
            }
            set
            {
                this.mediaTagPrefixField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint mediaTagVersion
        {
            get
            {
                return this.mediaTagVersionField;
            }
            set
            {
                this.mediaTagVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort playQueueID
        {
            get
            {
                return this.playQueueIDField;
            }
            set
            {
                this.playQueueIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort playQueueSelectedItemID
        {
            get
            {
                return this.playQueueSelectedItemIDField;
            }
            set
            {
                this.playQueueSelectedItemIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte playQueueSelectedItemOffset
        {
            get
            {
                return this.playQueueSelectedItemOffsetField;
            }
            set
            {
                this.playQueueSelectedItemOffsetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort playQueueSelectedMetadataItemID
        {
            get
            {
                return this.playQueueSelectedMetadataItemIDField;
            }
            set
            {
                this.playQueueSelectedMetadataItemIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte playQueueShuffled
        {
            get
            {
                return this.playQueueShuffledField;
            }
            set
            {
                this.playQueueShuffledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string playQueueSourceURI
        {
            get
            {
                return this.playQueueSourceURIField;
            }
            set
            {
                this.playQueueSourceURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte playQueueTotalCount
        {
            get
            {
                return this.playQueueTotalCountField;
            }
            set
            {
                this.playQueueTotalCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte playQueueVersion
        {
            get
            {
                return this.playQueueVersionField;
            }
            set
            {
                this.playQueueVersionField = value;
            }
        }
    }
}