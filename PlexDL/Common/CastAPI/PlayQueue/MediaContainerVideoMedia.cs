using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMedia
    {
        private MediaContainerVideoMediaPart partField;

        private ushort idField;

        private uint durationField;

        private ushort bitrateField;

        private ushort widthField;

        private ushort heightField;

        private decimal aspectRatioField;

        private byte audioChannelsField;

        private string audioCodecField;

        private string videoCodecField;

        private ushort videoResolutionField;

        private string containerField;

        private string videoFrameRateField;

        private string videoProfileField;

        /// <remarks/>
        public MediaContainerVideoMediaPart Part
        {
            get { return partField; }
            set { partField = value; }
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
        public uint duration
        {
            get { return durationField; }
            set { durationField = value; }
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
        public ushort width
        {
            get { return widthField; }
            set { widthField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort height
        {
            get { return heightField; }
            set { heightField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal aspectRatio
        {
            get { return aspectRatioField; }
            set { aspectRatioField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte audioChannels
        {
            get { return audioChannelsField; }
            set { audioChannelsField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string audioCodec
        {
            get { return audioCodecField; }
            set { audioCodecField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string videoCodec
        {
            get { return videoCodecField; }
            set { videoCodecField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort videoResolution
        {
            get { return videoResolutionField; }
            set { videoResolutionField = value; }
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
        public string videoFrameRate
        {
            get { return videoFrameRateField; }
            set { videoFrameRateField = value; }
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