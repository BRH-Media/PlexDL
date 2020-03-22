using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class XmlMetadataGatherers
    {
        public static XmlDocument GetSeriesXml(int index)
        {
            LoggingHelpers.AddToLog("Sorting existing data");

            DataRow result;

            result = RowGet.GetDataRowContent(index, true);

            var key = result["key"].ToString();
            var baseUri = GlobalStaticVars.GetBaseUri(false);
            key = key.TrimStart('/');
            var uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            LoggingHelpers.AddToLog("Contacting server");
            var reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public static XmlDocument GetEpisodeXml(int index)
        {
            LoggingHelpers.AddToLog("Sorting existing data");

            DataRow result;
            result = RowGet.GetDataRowSeries(index);

            var key = result["key"].ToString();
            var baseUri = GlobalStaticVars.GetBaseUri(false);
            key = key.TrimStart('/');
            var uri = baseUri + key + "/?X-Plex-Token=";

            //MessageBox.Show(uri);

            LoggingHelpers.AddToLog("Contacting server");
            var reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public static XmlDocument GetTVShowMetadata(int index)
        {
            LoggingHelpers.AddToLog("Sorting existing data");

            DataRow result;

            result = RowGet.GetDataRowSeries(index);

            var key = result["key"].ToString();
            var baseUri = GlobalStaticVars.GetBaseUri(false);
            key = key.TrimStart('/');
            var uri = baseUri + key + "/?X-Plex-Token=";

            LoggingHelpers.AddToLog("Contacting server");
            var reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public static List<PlexActor> GetActorsFromMetadata(XmlDocument metadata)
        {
            var actors = new List<PlexActor>();

            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var dtActors = sections.Tables["Role"];

            if (dtActors != null)
                foreach (DataRow r in dtActors.Rows)
                {
                    var a = new PlexActor();
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
                    a.ThumbnailUri = thumb;
                    a.ActorRole = role;
                    a.ActorName = name;
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
            if (video != null)
            {
                var row = video.Rows[0];
                if (!string.IsNullOrEmpty((string)row["tag"]))
                    genre = row["tag"].ToString();
            }

            return genre;
        }

        public static string GetContentSynopsis(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var synopsis = "Plot synopsis not provided";
            if (video != null)
            {
                var row = video.Rows[0];
                if (!string.IsNullOrEmpty((string)row["summary"]))
                    synopsis = row["summary"].ToString();
            }

            return synopsis;
        }

        public static string GetTVShowSeason(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var season = "Unknown Season";
            if (video != null)
            {
                var row = video.Rows[0];
                if (!string.IsNullOrEmpty((string)row["parentTitle"]))
                    season = row["parentTitle"].ToString();
            }

            return season;
        }

        public static string GetTVShowTitle(XmlDocument metadata)
        {
            var sections = new DataSet();
            sections.ReadXml(new XmlNodeReader(metadata));
            var video = sections.Tables["Video"];
            var title = "Unknown Title";
            if (video != null)
            {
                var row = video.Rows[0];
                if (!string.IsNullOrEmpty((string)row["grandparentTitle"]))
                    title = row["grandparentTitle"].ToString();
            }

            return title;
        }

        public static XmlDocument GetContentMetadata(int index)
        {
            LoggingHelpers.AddToLog("Sorting existing data");

            DataRow result;

            result = RowGet.GetDataRowContent(index, false);

            var key = result["key"].ToString();
            var baseUri = GlobalStaticVars.GetBaseUri(false);
            key = key.TrimStart('/');
            var uri = baseUri + key + "/?X-Plex-Token=";

            LoggingHelpers.AddToLog("Contacting server");
            var reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }

        public static XmlDocument GetEpisodeMetadata(int index)
        {
            LoggingHelpers.AddToLog("Sorting existing data");

            DataRow result;

            result = RowGet.GetDataRowEpisodes(index);

            var key = result["key"].ToString();
            var baseUri = GlobalStaticVars.GetBaseUri(false);
            key = key.TrimStart('/');
            var uri = baseUri + key + "/?X-Plex-Token=";

            LoggingHelpers.AddToLog("Contacting server");
            var reply = XmlGet.GetXMLTransaction(uri);
            return reply;
        }
    }
}