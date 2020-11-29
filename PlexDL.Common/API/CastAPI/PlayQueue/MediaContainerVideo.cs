using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.API.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideo
    {
        private uint addedAtField;

        private string artField;

        private decimal audienceRatingField;

        private string audienceRatingImageField;

        private string chapterSourceField;

        private string contentRatingField;

        private MediaContainerVideoCountry countryField;

        private MediaContainerVideoDirector directorField;

        private uint durationField;

        private MediaContainerVideoGenre[] genreField;

        private string guidField;

        private string keyField;

        private byte librarySectionIDField;

        private string librarySectionKeyField;

        private string librarySectionTitleField;
        private MediaContainerVideoMedia mediaField;

        private DateTime originallyAvailableAtField;

        private uint playQueueItemIDField;

        private MediaContainerVideoProducer[] producerField;

        private decimal ratingField;

        private string ratingImageField;

        private ushort ratingKeyField;

        private MediaContainerVideoRole[] roleField;

        private MediaContainerVideoSimilar[] similarField;

        private string studioField;

        private string summaryField;

        private string taglineField;

        private string thumbField;

        private ushort titleField;

        private string typeField;

        private uint updatedAtField;

        private MediaContainerVideoWriter[] writerField;

        private ushort yearField;

        /// <remarks />
        public MediaContainerVideoMedia Media
        {
            get => mediaField;
            set => mediaField = value;
        }

        /// <remarks />
        [XmlElement("Genre")]
        public MediaContainerVideoGenre[] Genre
        {
            get => genreField;
            set => genreField = value;
        }

        /// <remarks />
        public MediaContainerVideoDirector Director
        {
            get => directorField;
            set => directorField = value;
        }

        /// <remarks />
        [XmlElement("Writer")]
        public MediaContainerVideoWriter[] Writer
        {
            get => writerField;
            set => writerField = value;
        }

        /// <remarks />
        [XmlElement("Producer")]
        public MediaContainerVideoProducer[] Producer
        {
            get => producerField;
            set => producerField = value;
        }

        /// <remarks />
        public MediaContainerVideoCountry Country
        {
            get => countryField;
            set => countryField = value;
        }

        /// <remarks />
        [XmlElement("Role")]
        public MediaContainerVideoRole[] Role
        {
            get => roleField;
            set => roleField = value;
        }

        /// <remarks />
        [XmlElement("Similar")]
        public MediaContainerVideoSimilar[] Similar
        {
            get => similarField;
            set => similarField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint playQueueItemID
        {
            get => playQueueItemIDField;
            set => playQueueItemIDField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort ratingKey
        {
            get => ratingKeyField;
            set => ratingKeyField = value;
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
        public string guid
        {
            get => guidField;
            set => guidField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string studio
        {
            get => studioField;
            set => studioField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string type
        {
            get => typeField;
            set => typeField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort title
        {
            get => titleField;
            set => titleField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string librarySectionTitle
        {
            get => librarySectionTitleField;
            set => librarySectionTitleField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public byte librarySectionID
        {
            get => librarySectionIDField;
            set => librarySectionIDField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string librarySectionKey
        {
            get => librarySectionKeyField;
            set => librarySectionKeyField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string contentRating
        {
            get => contentRatingField;
            set => contentRatingField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string summary
        {
            get => summaryField;
            set => summaryField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public decimal rating
        {
            get => ratingField;
            set => ratingField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public decimal audienceRating
        {
            get => audienceRatingField;
            set => audienceRatingField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public ushort year
        {
            get => yearField;
            set => yearField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string tagline
        {
            get => taglineField;
            set => taglineField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string thumb
        {
            get => thumbField;
            set => thumbField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string art
        {
            get => artField;
            set => artField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint duration
        {
            get => durationField;
            set => durationField = value;
        }

        /// <remarks />
        [XmlAttribute(DataType = "date")]
        public DateTime originallyAvailableAt
        {
            get => originallyAvailableAtField;
            set => originallyAvailableAtField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint addedAt
        {
            get => addedAtField;
            set => addedAtField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public uint updatedAt
        {
            get => updatedAtField;
            set => updatedAtField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string audienceRatingImage
        {
            get => audienceRatingImageField;
            set => audienceRatingImageField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string chapterSource
        {
            get => chapterSourceField;
            set => chapterSourceField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string ratingImage
        {
            get => ratingImageField;
            set => ratingImageField = value;
        }
    }
}