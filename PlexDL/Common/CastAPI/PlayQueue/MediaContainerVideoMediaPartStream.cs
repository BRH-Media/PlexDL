using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMediaPartStream
    {
        private uint idField;

        private byte streamTypeField;

        private byte defaultField;

        private string codecField;

        private byte indexField;

        private ushort bitrateField;

        private byte bitDepthField;

        private bool bitDepthFieldSpecified;

        private string chromaLocationField;

        private string chromaSubsamplingField;

        private ushort codedHeightField;

        private bool codedHeightFieldSpecified;

        private ushort codedWidthField;

        private bool codedWidthFieldSpecified;

        private decimal frameRateField;

        private bool frameRateFieldSpecified;

        private byte hasScalingMatrixField;

        private bool hasScalingMatrixFieldSpecified;

        private ushort heightField;

        private bool heightFieldSpecified;

        private byte levelField;

        private bool levelFieldSpecified;

        private string profileField;

        private byte refFramesField;

        private bool refFramesFieldSpecified;

        private string scanTypeField;

        private ushort widthField;

        private bool widthFieldSpecified;

        private string displayTitleField;

        private byte selectedField;

        private bool selectedFieldSpecified;

        private byte channelsField;

        private bool channelsFieldSpecified;

        private string languageField;

        private string languageCodeField;

        private string audioChannelLayoutField;

        private ushort samplingRateField;

        private bool samplingRateFieldSpecified;

        /// <remarks/>
        [XmlAttribute]
        public uint id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte streamType
        {
            get { return streamTypeField; }
            set { streamTypeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte @default
        {
            get { return defaultField; }
            set { defaultField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string codec
        {
            get { return codecField; }
            set { codecField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte index
        {
            get { return indexField; }
            set { indexField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort bitrate
        {
            get { return bitrateField; }
            set { bitrateField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte bitDepth
        {
            get { return bitDepthField; }
            set { bitDepthField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool bitDepthSpecified
        {
            get { return bitDepthFieldSpecified; }
            set { bitDepthFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string chromaLocation
        {
            get { return chromaLocationField; }
            set { chromaLocationField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string chromaSubsampling
        {
            get { return chromaSubsamplingField; }
            set { chromaSubsamplingField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort codedHeight
        {
            get { return codedHeightField; }
            set { codedHeightField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool codedHeightSpecified
        {
            get { return codedHeightFieldSpecified; }
            set { codedHeightFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort codedWidth
        {
            get { return codedWidthField; }
            set { codedWidthField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool codedWidthSpecified
        {
            get { return codedWidthFieldSpecified; }
            set { codedWidthFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal frameRate
        {
            get { return frameRateField; }
            set { frameRateField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool frameRateSpecified
        {
            get { return frameRateFieldSpecified; }
            set { frameRateFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte hasScalingMatrix
        {
            get { return hasScalingMatrixField; }
            set { hasScalingMatrixField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool hasScalingMatrixSpecified
        {
            get { return hasScalingMatrixFieldSpecified; }
            set { hasScalingMatrixFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort height
        {
            get { return heightField; }
            set { heightField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool heightSpecified
        {
            get { return heightFieldSpecified; }
            set { heightFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte level
        {
            get { return levelField; }
            set { levelField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool levelSpecified
        {
            get { return levelFieldSpecified; }
            set { levelFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string profile
        {
            get { return profileField; }
            set { profileField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte refFrames
        {
            get { return refFramesField; }
            set { refFramesField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool refFramesSpecified
        {
            get { return refFramesFieldSpecified; }
            set { refFramesFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string scanType
        {
            get { return scanTypeField; }
            set { scanTypeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort width
        {
            get { return widthField; }
            set { widthField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool widthSpecified
        {
            get { return widthFieldSpecified; }
            set { widthFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string displayTitle
        {
            get { return displayTitleField; }
            set { displayTitleField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte selected
        {
            get { return selectedField; }
            set { selectedField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool selectedSpecified
        {
            get { return selectedFieldSpecified; }
            set { selectedFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte channels
        {
            get { return channelsField; }
            set { channelsField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool channelsSpecified
        {
            get { return channelsFieldSpecified; }
            set { channelsFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string language
        {
            get { return languageField; }
            set { languageField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string languageCode
        {
            get { return languageCodeField; }
            set { languageCodeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string audioChannelLayout
        {
            get { return audioChannelLayoutField; }
            set { audioChannelLayoutField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort samplingRate
        {
            get { return samplingRateField; }
            set { samplingRateField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool samplingRateSpecified
        {
            get { return samplingRateFieldSpecified; }
            set { samplingRateFieldSpecified = value; }
        }
    }
}