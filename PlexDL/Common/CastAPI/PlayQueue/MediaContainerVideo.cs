using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideo
    {
        private MediaContainerVideoMedia mediaField;

        private MediaContainerVideoGenre[] genreField;

        private MediaContainerVideoDirector directorField;

        private MediaContainerVideoWriter[] writerField;

        private MediaContainerVideoProducer[] producerField;

        private MediaContainerVideoCountry countryField;

        private MediaContainerVideoRole[] roleField;

        private MediaContainerVideoSimilar[] similarField;

        private uint playQueueItemIDField;

        private ushort ratingKeyField;

        private string keyField;

        private string guidField;

        private string studioField;

        private string typeField;

        private ushort titleField;

        private string librarySectionTitleField;

        private byte librarySectionIDField;

        private string librarySectionKeyField;

        private string contentRatingField;

        private string summaryField;

        private decimal ratingField;

        private decimal audienceRatingField;

        private ushort yearField;

        private string taglineField;

        private string thumbField;

        private string artField;

        private uint durationField;

        private DateTime originallyAvailableAtField;

        private uint addedAtField;

        private uint updatedAtField;

        private string audienceRatingImageField;

        private string chapterSourceField;

        private string ratingImageField;

        /// <remarks/>
        public MediaContainerVideoMedia Media
        {
            get { return mediaField; }
            set { mediaField = value; }
        }

        /// <remarks/>
        [XmlElement("Genre")]
        public MediaContainerVideoGenre[] Genre
        {
            get { return genreField; }
            set { genreField = value; }
        }

        /// <remarks/>
        public MediaContainerVideoDirector Director
        {
            get { return directorField; }
            set { directorField = value; }
        }

        /// <remarks/>
        [XmlElement("Writer")]
        public MediaContainerVideoWriter[] Writer
        {
            get { return writerField; }
            set { writerField = value; }
        }

        /// <remarks/>
        [XmlElement("Producer")]
        public MediaContainerVideoProducer[] Producer
        {
            get { return producerField; }
            set { producerField = value; }
        }

        /// <remarks/>
        public MediaContainerVideoCountry Country
        {
            get { return countryField; }
            set { countryField = value; }
        }

        /// <remarks/>
        [XmlElement("Role")]
        public MediaContainerVideoRole[] Role
        {
            get { return roleField; }
            set { roleField = value; }
        }

        /// <remarks/>
        [XmlElement("Similar")]
        public MediaContainerVideoSimilar[] Similar
        {
            get { return similarField; }
            set { similarField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint playQueueItemID
        {
            get { return playQueueItemIDField; }
            set { playQueueItemIDField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort ratingKey
        {
            get { return ratingKeyField; }
            set { ratingKeyField = value; }
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
        public string guid
        {
            get { return guidField; }
            set { guidField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string studio
        {
            get { return studioField; }
            set { studioField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string type
        {
            get { return typeField; }
            set { typeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort title
        {
            get { return titleField; }
            set { titleField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string librarySectionTitle
        {
            get { return librarySectionTitleField; }
            set { librarySectionTitleField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte librarySectionID
        {
            get { return librarySectionIDField; }
            set { librarySectionIDField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string librarySectionKey
        {
            get { return librarySectionKeyField; }
            set { librarySectionKeyField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string contentRating
        {
            get { return contentRatingField; }
            set { contentRatingField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string summary
        {
            get { return summaryField; }
            set { summaryField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal rating
        {
            get { return ratingField; }
            set { ratingField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal audienceRating
        {
            get { return audienceRatingField; }
            set { audienceRatingField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort year
        {
            get { return yearField; }
            set { yearField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string tagline
        {
            get { return taglineField; }
            set { taglineField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string thumb
        {
            get { return thumbField; }
            set { thumbField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string art
        {
            get { return artField; }
            set { artField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint duration
        {
            get { return durationField; }
            set { durationField = value; }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "date")]
        public DateTime originallyAvailableAt
        {
            get { return originallyAvailableAtField; }
            set { originallyAvailableAtField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint addedAt
        {
            get { return addedAtField; }
            set { addedAtField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint updatedAt
        {
            get { return updatedAtField; }
            set { updatedAtField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string audienceRatingImage
        {
            get { return audienceRatingImageField; }
            set { audienceRatingImageField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string chapterSource
        {
            get { return chapterSourceField; }
            set { chapterSourceField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string ratingImage
        {
            get { return ratingImageField; }
            set { ratingImageField = value; }
        }
    }
}