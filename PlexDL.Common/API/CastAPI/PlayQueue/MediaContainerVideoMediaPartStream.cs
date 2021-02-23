namespace PlexDL.Common.API.CastAPI.PlayQueue
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MediaContainerVideoMediaPartStream
    {
        private uint idField;

        private byte streamTypeField;

        private byte defaultField;

        private bool defaultFieldSpecified;

        private string codecField;

        private byte indexField;

        private ushort bitrateField;

        private bool bitrateFieldSpecified;

        private byte bitDepthField;

        private bool bitDepthFieldSpecified;

        private string chromaSubsamplingField;

        private ushort codedHeightField;

        private bool codedHeightFieldSpecified;

        private ushort codedWidthField;

        private bool codedWidthFieldSpecified;

        private string colorPrimariesField;

        private string colorRangeField;

        private string colorSpaceField;

        private string colorTrcField;

        private decimal frameRateField;

        private bool frameRateFieldSpecified;

        private ushort heightField;

        private bool heightFieldSpecified;

        private byte levelField;

        private bool levelFieldSpecified;

        private string profileField;

        private byte refFramesField;

        private bool refFramesFieldSpecified;

        private ushort widthField;

        private bool widthFieldSpecified;

        private string displayTitleField;

        private string extendedDisplayTitleField;

        private byte selectedField;

        private bool selectedFieldSpecified;

        private byte channelsField;

        private bool channelsFieldSpecified;

        private string audioChannelLayoutField;

        private ushort samplingRateField;

        private bool samplingRateFieldSpecified;

        private string titleField;

        private string languageField;

        private string languageCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte streamType
        {
            get
            {
                return this.streamTypeField;
            }
            set
            {
                this.streamTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool defaultSpecified
        {
            get
            {
                return this.defaultFieldSpecified;
            }
            set
            {
                this.defaultFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codec
        {
            get
            {
                return this.codecField;
            }
            set
            {
                this.codecField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort bitrate
        {
            get
            {
                return this.bitrateField;
            }
            set
            {
                this.bitrateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool bitrateSpecified
        {
            get
            {
                return this.bitrateFieldSpecified;
            }
            set
            {
                this.bitrateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte bitDepth
        {
            get
            {
                return this.bitDepthField;
            }
            set
            {
                this.bitDepthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool bitDepthSpecified
        {
            get
            {
                return this.bitDepthFieldSpecified;
            }
            set
            {
                this.bitDepthFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string chromaSubsampling
        {
            get
            {
                return this.chromaSubsamplingField;
            }
            set
            {
                this.chromaSubsamplingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codedHeight
        {
            get
            {
                return this.codedHeightField;
            }
            set
            {
                this.codedHeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool codedHeightSpecified
        {
            get
            {
                return this.codedHeightFieldSpecified;
            }
            set
            {
                this.codedHeightFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codedWidth
        {
            get
            {
                return this.codedWidthField;
            }
            set
            {
                this.codedWidthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool codedWidthSpecified
        {
            get
            {
                return this.codedWidthFieldSpecified;
            }
            set
            {
                this.codedWidthFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colorPrimaries
        {
            get
            {
                return this.colorPrimariesField;
            }
            set
            {
                this.colorPrimariesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colorRange
        {
            get
            {
                return this.colorRangeField;
            }
            set
            {
                this.colorRangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colorSpace
        {
            get
            {
                return this.colorSpaceField;
            }
            set
            {
                this.colorSpaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colorTrc
        {
            get
            {
                return this.colorTrcField;
            }
            set
            {
                this.colorTrcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal frameRate
        {
            get
            {
                return this.frameRateField;
            }
            set
            {
                this.frameRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool frameRateSpecified
        {
            get
            {
                return this.frameRateFieldSpecified;
            }
            set
            {
                this.frameRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool heightSpecified
        {
            get
            {
                return this.heightFieldSpecified;
            }
            set
            {
                this.heightFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool levelSpecified
        {
            get
            {
                return this.levelFieldSpecified;
            }
            set
            {
                this.levelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string profile
        {
            get
            {
                return this.profileField;
            }
            set
            {
                this.profileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte refFrames
        {
            get
            {
                return this.refFramesField;
            }
            set
            {
                this.refFramesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refFramesSpecified
        {
            get
            {
                return this.refFramesFieldSpecified;
            }
            set
            {
                this.refFramesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool widthSpecified
        {
            get
            {
                return this.widthFieldSpecified;
            }
            set
            {
                this.widthFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string displayTitle
        {
            get
            {
                return this.displayTitleField;
            }
            set
            {
                this.displayTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string extendedDisplayTitle
        {
            get
            {
                return this.extendedDisplayTitleField;
            }
            set
            {
                this.extendedDisplayTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte selected
        {
            get
            {
                return this.selectedField;
            }
            set
            {
                this.selectedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool selectedSpecified
        {
            get
            {
                return this.selectedFieldSpecified;
            }
            set
            {
                this.selectedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte channels
        {
            get
            {
                return this.channelsField;
            }
            set
            {
                this.channelsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool channelsSpecified
        {
            get
            {
                return this.channelsFieldSpecified;
            }
            set
            {
                this.channelsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string audioChannelLayout
        {
            get
            {
                return this.audioChannelLayoutField;
            }
            set
            {
                this.audioChannelLayoutField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort samplingRate
        {
            get
            {
                return this.samplingRateField;
            }
            set
            {
                this.samplingRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool samplingRateSpecified
        {
            get
            {
                return this.samplingRateFieldSpecified;
            }
            set
            {
                this.samplingRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }
    }
}