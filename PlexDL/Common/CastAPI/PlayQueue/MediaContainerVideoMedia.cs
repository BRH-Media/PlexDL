using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoMedia
    {
        private decimal aspectRatioField;

        private byte audioChannelsField;

        private string audioCodecField;

        private ushort bitrateField;

        private string containerField;

        private uint durationField;

        private ushort heightField;

        private ushort idField;
        private MediaContainerVideoMediaPart partField;

        private string videoCodecField;

        private string videoFrameRateField;

        private string videoProfileField;

        private ushort videoResolutionField;

        private ushort widthField;

        /// <remarks />
        public MediaContainerVideoMediaPart Part
        {
            get => partField;
            set => partField = value;
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
        public uint duration
        {
            get => durationField;
            set => durationField = value;
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
        public ushort width
        {
            get => widthField;
            set => widthField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort height
        {
            get => heightField;
            set => heightField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public decimal aspectRatio
        {
            get => aspectRatioField;
            set => aspectRatioField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte audioChannels
        {
            get => audioChannelsField;
            set => audioChannelsField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string audioCodec
        {
            get => audioCodecField;
            set => audioCodecField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string videoCodec
        {
            get => videoCodecField;
            set => videoCodecField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort videoResolution
        {
            get => videoResolutionField;
            set => videoResolutionField = value;
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
        public string videoFrameRate
        {
            get => videoFrameRateField;
            set => videoFrameRateField = value;
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