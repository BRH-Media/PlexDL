using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMediaPartStream
    {
        private string audioChannelLayoutField;

        private byte bitDepthField;

        private bool bitDepthFieldSpecified;

        private ushort bitrateField;

        private byte channelsField;

        private bool channelsFieldSpecified;

        private string chromaLocationField;

        private string chromaSubsamplingField;

        private string codecField;

        private ushort codedHeightField;

        private bool codedHeightFieldSpecified;

        private ushort codedWidthField;

        private bool codedWidthFieldSpecified;

        private byte defaultField;

        private string displayTitleField;

        private decimal frameRateField;

        private bool frameRateFieldSpecified;

        private byte hasScalingMatrixField;

        private bool hasScalingMatrixFieldSpecified;

        private ushort heightField;

        private bool heightFieldSpecified;
        private uint idField;

        private byte indexField;

        private string languageCodeField;

        private string languageField;

        private byte levelField;

        private bool levelFieldSpecified;

        private string profileField;

        private byte refFramesField;

        private bool refFramesFieldSpecified;

        private ushort samplingRateField;

        private bool samplingRateFieldSpecified;

        private string scanTypeField;

        private byte selectedField;

        private bool selectedFieldSpecified;

        private byte streamTypeField;

        private ushort widthField;

        private bool widthFieldSpecified;

        /// <remarks />
        [XmlAttribute]
        public uint id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte streamType
        {
            get => streamTypeField;
            set => streamTypeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte @default
        {
            get => defaultField;
            set => defaultField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string codec
        {
            get => codecField;
            set => codecField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte index
        {
            get => indexField;
            set => indexField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort bitrate
        {
            get => bitrateField;
            set => bitrateField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte bitDepth
        {
            get => bitDepthField;
            set => bitDepthField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool bitDepthSpecified
        {
            get => bitDepthFieldSpecified;
            set => bitDepthFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string chromaLocation
        {
            get => chromaLocationField;
            set => chromaLocationField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string chromaSubsampling
        {
            get => chromaSubsamplingField;
            set => chromaSubsamplingField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort codedHeight
        {
            get => codedHeightField;
            set => codedHeightField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool codedHeightSpecified
        {
            get => codedHeightFieldSpecified;
            set => codedHeightFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort codedWidth
        {
            get => codedWidthField;
            set => codedWidthField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool codedWidthSpecified
        {
            get => codedWidthFieldSpecified;
            set => codedWidthFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public decimal frameRate
        {
            get => frameRateField;
            set => frameRateField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool frameRateSpecified
        {
            get => frameRateFieldSpecified;
            set => frameRateFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte hasScalingMatrix
        {
            get => hasScalingMatrixField;
            set => hasScalingMatrixField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool hasScalingMatrixSpecified
        {
            get => hasScalingMatrixFieldSpecified;
            set => hasScalingMatrixFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort height
        {
            get => heightField;
            set => heightField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool heightSpecified
        {
            get => heightFieldSpecified;
            set => heightFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte level
        {
            get => levelField;
            set => levelField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool levelSpecified
        {
            get => levelFieldSpecified;
            set => levelFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string profile
        {
            get => profileField;
            set => profileField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte refFrames
        {
            get => refFramesField;
            set => refFramesField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool refFramesSpecified
        {
            get => refFramesFieldSpecified;
            set => refFramesFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string scanType
        {
            get => scanTypeField;
            set => scanTypeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort width
        {
            get => widthField;
            set => widthField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool widthSpecified
        {
            get => widthFieldSpecified;
            set => widthFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string displayTitle
        {
            get => displayTitleField;
            set => displayTitleField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte selected
        {
            get => selectedField;
            set => selectedField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool selectedSpecified
        {
            get => selectedFieldSpecified;
            set => selectedFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte channels
        {
            get => channelsField;
            set => channelsField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool channelsSpecified
        {
            get => channelsFieldSpecified;
            set => channelsFieldSpecified = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string language
        {
            get => languageField;
            set => languageField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string languageCode
        {
            get => languageCodeField;
            set => languageCodeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string audioChannelLayout
        {
            get => audioChannelLayoutField;
            set => audioChannelLayoutField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort samplingRate
        {
            get => samplingRateField;
            set => samplingRateField = value;
        }

        /// <remarks />
        [XmlIgnore]
        public bool samplingRateSpecified
        {
            get => samplingRateFieldSpecified;
            set => samplingRateFieldSpecified = value;
        }
    }
}