using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class XmlMetadataGatherers
    {
        public static XmlDocument GetMetadata(DataRow result, string msgNoKey = "Error occurred whilst getting the unique content key",
            string logNoKeyMsg = "Error occurred whilst getting the unique content key", string logNoKeyType = "NoUnqKeyError", string column = "key")
        {
            XmlDocument reply = null;

            var key = "";

            if (result[column] != null)
                if (!Equals(result[column], string.Empty))
                    key = result[column].ToString();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show(msgNoKey, @"Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordGenericEntry("Unique key error");
                LoggingHelpers.RecordException(logNoKeyMsg, logNoKeyType);
            }
            else
            {
                var baseUri = GlobalStaticVars.GetBaseUri(false);
                key = key.TrimStart('/');
                var uri = baseUri + key + "/?X-Plex-Token=";

                LoggingHelpers.RecordGenericEntry("Contacting the API");
                reply = XmlGet.GetXmlTransaction(uri);
            }

            return reply;
        }

        public static List<PlexActor> GetActorsFromMetadata(XmlDocument metadata)
        {
            var actors = new List<PlexActor>();

            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var dtActors = sections.Tables["Role"];

            if (dtActors == null)
                return actors;
            foreach (DataRow r in dtActors.Rows)
            {
                var thumb = "";
                var role = "Unknown";
                var name = "Unknown";
                if (dtActors.Columns.Contains("thumb"))
                    if (r["thumb"] != null)
                        thumb = r["thumb"].ToString();
                if (dtActors.Columns.Contains("role"))
                    if (r["role"] != null)
                        role = r["role"].ToString();
                if (dtActors.Columns.Contains("tag"))
                    if (r["tag"] != null)
                        name = r["tag"].ToString();
                var a = new PlexActor
                {
                    ThumbnailUri = thumb,
                    ActorRole = role,
                    ActorName = name
                };
                actors.Add(a);
            }

            return actors;
        }

        public static Resolution GetContentResolution(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Media"];
            var row = video.Rows[0];

            var width = 0;
            if (video.Columns.Contains("width"))
                if (row["width"] != null)
                    width = Convert.ToInt32(row["width"]);

            var height = 0;
            if (video.Columns.Contains("height"))
                if (row["height"] != null)
                    height = Convert.ToInt32(row["height"]);

            var fps = "";
            if (video.Columns.Contains("videoFrameRate"))
                if (row["videoFrameRate"] != null)
                    fps = ResolutionStandards.FpsFromPlexStd(row["videoFrameRate"].ToString().ToLower(), false);

            var result = new Resolution
            {
                Width = width,
                Height = height,
                Framerate = fps,
                VideoStandard = ResolutionStandards.StdFromHeight(height.ToString())
            };
            return result;
        }

        public static string GetContentGenre(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Genre"];
            var genre = "Unknown";
            if (video == null)
                return genre;
            var row = video.Rows[0];
            if (row["tag"] != null)
                if (!Equals(row["tag"], string.Empty))
                    genre = row["tag"].ToString();

            return genre;
        }

        public static string GetContentSynopsis(XmlDocument metadata)
        {
            var attr = "summary";
            var def = @"Plot synopsis not provided";
            var contentType = GlobalStaticVars.CurrentContentType;
            return GetContentAttribute(metadata, contentType, attr, def);
        }

        public static string GetContentAttribute(XmlDocument metadata, ContentType type, string attributeName, string defaultValue = @"Unknown")
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            DataTable data = null;

            switch (type)
            {
                case ContentType.MOVIES:
                    data = sections.Tables["Video"];
                    break;

                case ContentType.TV_SHOWS:
                    data = sections.Tables["Video"];
                    break;

                case ContentType.MUSIC:
                    data = sections.Tables["Track"];
                    break;
            }

            var attributeValue = defaultValue;

            //return the default value above if the data (table) is null
            if (data == null)
                return attributeValue;

            //the first row is the only information we will need
            var row = data.Rows[0];

            //first, check if the specified attribute exists in the row data
            if (row[attributeName] != null)
                if (!Equals(row[attributeName], string.Empty))
                    attributeValue = row[attributeName].ToString();

            return attributeValue;
        }

        public static string GetParentTitle(XmlDocument metadata)
        {
            var attr = "parentTitle";
            var contentType = GlobalStaticVars.CurrentContentType;
            return GetContentAttribute(metadata, contentType, attr);
        }

        public static string GetGrandparentTitle(XmlDocument metadata)
        {
            var attr = "grandparentTitle";
            var contentType = GlobalStaticVars.CurrentContentType;
            return GetContentAttribute(metadata, contentType, attr);
        }

        public static XmlDocument GetSeriesXml(int index)
        {
            XmlDocument doc;
            try
            {
                LoggingHelpers.RecordGenericEntry("Getting series list");

                var result = RowGet.GetDataRowContent(index);

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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

                doc = GetMetadata(result);
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