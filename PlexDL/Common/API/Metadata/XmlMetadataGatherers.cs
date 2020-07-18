using PlexDL.Common.API.Metadata.Objects;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;

namespace PlexDL.Common.API.Metadata
{
    public static class XmlMetadataGatherers
    {
        public static XmlMetadata GetSeriesXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting series list");

                var result = RowGet.GetDataRowContent(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetSeriesListError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetAlbumsXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting album list");

                var result = RowGet.GetDataRowContent(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetAlbumListError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetEpisodeXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting episodes list");

                var result = RowGet.GetDataRowSeries(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetEpisodesListError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetTracksXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting track list");

                var result = RowGet.GetDataRowAlbums(index);

                obj = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetTrackListError");
                obj = new XmlMetadata();
            }

            return obj;
        }

        public static XmlMetadata GetContentMetadata(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting movie metadata");

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
                LoggingHelpers.RecordGeneralEntry("Getting episode metadata");

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
                LoggingHelpers.RecordGeneralEntry("Getting track metadata");

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