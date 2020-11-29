using PlexDL.Common.API.PlexAPI.Metadata.Objects;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;

namespace PlexDL.Common.API.PlexAPI.Metadata.Handlers
{
    public static class XmlMetadataLists
    {
        public static XmlMetadata GetSeriesListXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting series list XML");

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

        public static XmlMetadata GetAlbumsListXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting album list XML");

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

        public static XmlMetadata GetEpisodeListXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting episodes list XML");

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

        public static XmlMetadata GetTracksListXml(int index)
        {
            XmlMetadata obj;
            try
            {
                LoggingHelpers.RecordGeneralEntry("Getting track list XML");

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
    }
}