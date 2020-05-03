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
            var result = new Resolution
            {
                Width = width,
                Height = height
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
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var synopsis = "Plot synopsis not provided";
            if (video == null)
                return synopsis;
            var row = video.Rows[0];
            if (row["summary"] != null)
                if (!Equals(row["summary"], string.Empty))
                    synopsis = row["summary"].ToString();

            return synopsis;
        }

        public static string GetTvShowSeason(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var season = "Unknown Season";
            if (video == null)
                return season;
            var row = video.Rows[0];
            if (row["parentTitle"] != null)
                if (!Equals(row["parentTitle"], string.Empty))
                    season = row["parentTitle"].ToString();

            return season;
        }

        public static string GetTvShowTitle(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var title = "Unknown Title";
            if (video == null)
                return title;
            var row = video.Rows[0];
            if (row["grandparentTitle"] != null)
                if (!Equals(row["grandparentTitle"], string.Empty))
                    title = row["grandparentTitle"].ToString();

            return title;
        }

        public static XmlDocument GetSeriesXml(int index)
        {
            XmlDocument doc;
            //try
            //{
            LoggingHelpers.RecordGenericEntry("Getting series list");

            var result = RowGet.GetDataRowContent(index);

            doc = GetMetadata(result);
            /*
        }
        catch (Exception ex)
        {
            LoggingHelpers.RecordException(ex.Message, "GetSeriesListError");
            doc = new XmlDocument();
        }
        */
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
    }
}