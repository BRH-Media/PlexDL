using PlexDL.Common.API.PlexAPI.Metadata.Objects;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;

namespace PlexDL.Common.API.PlexAPI.Metadata.Handlers
{
    public static class XmlMetadataContent
    {
        public static XmlMetadata GetContentMetadata(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting movie metadata XML");

                var result = RowGet.GetDataRowContent(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetMovieMetadataError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetEpisodeMetadata(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting episode metadata XML");

                var result = RowGet.GetDataRowEpisodes(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetEpisodeMetadataError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetTrackMetadata(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting track metadata XML");

                var result = RowGet.GetDataRowTracks(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetTrackMetadataError");
                obj = new XmlMetadata();
            }

            return obj;
        }
    }
}