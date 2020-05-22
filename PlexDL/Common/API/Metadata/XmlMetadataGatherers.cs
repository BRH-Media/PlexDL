using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.Xml;

namespace PlexDL.Common.API.Metadata
{
    public static class XmlMetadataGatherers
    {
        public static XmlDocument GetSeriesXml(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting series list");

                var result = RowGet.GetDataRowContent(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetSeriesListError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetAlbumsXml(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting album list");

                var result = RowGet.GetDataRowContent(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetAlbumListError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetEpisodeXml(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting episodes list");

                var result = RowGet.GetDataRowSeries(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetEpisodesListError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetTracksXml(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting track list");

                var result = RowGet.GetDataRowAlbums(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetTrackListError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetContentMetadata(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting movie metadata");

                var result = RowGet.GetDataRowContent(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetMovieMetadataError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetEpisodeMetadata(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting episode metadata");

                var result = RowGet.GetDataRowEpisodes(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetEpisodeMetadataError");
                doc = new XmlDocument();
            }

            return doc;
        }

        public static XmlDocument GetTrackMetadata(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting track metadata");

                var result = RowGet.GetDataRowTracks(index);

                doc = XmlMetadataHelpers.GetMetadata(result);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "GetTrackMetadataError");
                doc = new XmlDocument();
            }

            return doc;
        }
    }
}