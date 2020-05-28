using PlexDL.Common.API.Metadata;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.API
{
    public static class ObjectBuilders
    {
        public static DataTable AttributesFromObject(object content, bool silent = false)
        {
            var table = new DataTable("Attributes");
            var ColumnAttributeName = new DataColumn("Name", typeof(string));
            var ColumnAttributeValue = new DataColumn("Value");
            table.Columns.AddRange(
                new DataColumn[]
                {
                    ColumnAttributeName,
                    ColumnAttributeValue
                });
            try
            {
                var contentType = content.GetType();
                var moviesType = typeof(PlexMovie);
                var tracksType = typeof(PlexMusic);
                var tvShowType = typeof(PlexTVShow);

                if (contentType == moviesType)
                {
                    var contentConverted = (PlexMovie)content;

                    var genre = new[] { "Genre", contentConverted.ContentGenre };
                    var runtime = new[] { "Runtime", Methods.CalculateTime(contentConverted.StreamInformation.ContentDuration) };
                    var resolution = new[] { "Resolution", contentConverted.StreamResolution.ResolutionString() };
                    var framerate = new[] { "Frame-rate", Framerate(contentConverted) };
                    var size = new[] { "File size", Methods.FormatBytes(contentConverted.StreamInformation.ByteLength) };
                    var container = new[] { "Container", contentConverted.StreamInformation.Container };

                    var newRows = new[]
                    {
                        genre,
                        runtime,
                        resolution,
                        framerate,
                        size,
                        container
                    };

                    foreach (object[] row in newRows)
                        table.Rows.Add(row);
                }
                else if (contentType == tracksType)
                {
                    var contentConverted = (PlexMusic)content;

                    var artist = new[] { "Artist", contentConverted.Artist };
                    var album = new[] { "Album", contentConverted.Album };
                    var genre = new[] { "Genre", contentConverted.ContentGenre };
                    var duration = new[] { "Duration", Methods.CalculateTime(contentConverted.StreamInformation.ContentDuration) };
                    var size = new[] { "File size", Methods.FormatBytes(contentConverted.StreamInformation.ByteLength) };
                    var container = new[] { "Container", contentConverted.StreamInformation.Container };

                    var newRows = new[]
                    {
                        artist,
                        album,
                        genre,
                        duration,
                        size,
                        container
                    };

                    foreach (object[] row in newRows)
                        table.Rows.Add(row);
                }
                else if (contentType == tvShowType)
                {
                    var contentConverted = (PlexTVShow)content;

                    var season = new[] { "Season", contentConverted.Season };
                    var totalEpisodes = new[] { "Episode Count", contentConverted.EpisodesInSeason.ToString() };
                    var episode = new[] { "Episode", contentConverted.Episode };
                    var genre = new[] { "Genre", contentConverted.ContentGenre };
                    var runtime = new[] { "Runtime", Methods.CalculateTime(contentConverted.StreamInformation.ContentDuration) };
                    var resolution = new[] { "Resolution", contentConverted.StreamResolution.ResolutionString() };
                    var framerate = new[] { "Frame-rate", Framerate(contentConverted) };
                    var size = new[] { "File size", Methods.FormatBytes(contentConverted.StreamInformation.ByteLength) };
                    var container = new[] { "Container", contentConverted.StreamInformation.Container };
                    

                    var newRows = new[]
                    {
                        season,
                        totalEpisodes,
                        episode,
                        genre,
                        runtime,
                        resolution,
                        framerate,
                        size,
                        container
                    };

                    foreach (object[] row in newRows)
                        table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "AttributeTableError");
                if (!silent)
                    MessageBox.Show("Error occurred whilst building content attribute table:\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        private static DataTable GenericAttributesTable(bool silent = false)
        {
            DataTable table = new DataTable();

            try
            {

            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "AttributeTableError");
                if (!silent)
                    MessageBox.Show("Error occurred whilst building content attribute table:\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        //just to make it easier :)
        private static string Framerate(PlexObject streamingContent)
        {
            if (!string.Equals(streamingContent.StreamResolution.Framerate, "Unknown"))
                return ResolutionStandards.FullFpsSuffix(streamingContent.StreamResolution.Framerate);
            return "Unknown";
        }

        public static PlexTVShow GetTvObjectFromIndex(int index, bool formatLinkDownload)
        {
            try
            {
                var obj = new PlexTVShow();
                LoggingHelpers.RecordGenericEntry(@"Content Parse Started");
                LoggingHelpers.RecordGenericEntry(@"Grabbing Titles");

                var metadata = XmlMetadataGatherers.GetEpisodeMetadata(index);

                LoggingHelpers.RecordGenericEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.RecordGenericEntry(@"XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata, formatLinkDownload);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGenericEntry(@"Assembling Object");

                        obj.ContentGenre = XmlMetadataParsers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.Season = XmlMetadataParsers.GetParentTitle(metadata);
                        obj.EpisodesInSeason = GlobalTables.EpisodesTable.Rows.Count;
                        obj.TVShowName = XmlMetadataParsers.GetGrandparentTitle(metadata);
                        obj.StreamResolution = XmlMetadataParsers.GetContentResolution(metadata);
                        obj.Actors = XmlMetadataParsers.GetActorsFromMetadata(metadata);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataParsers.GetContentSynopsis(metadata);
                    }
                    else
                    {
                        MessageBox.Show(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            "DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            "ContextDownloadInfoNull");
                        LoggingHelpers.RecordGenericEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGenericEntry("XML Invalid");
                }

                LoggingHelpers.RecordGenericEntry("Returned assembled TV object");
                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Content metadata error:\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "TVObjectError");
                return null;
            }
        }

        public static PlexMovie GetMovieObjectFromIndex(int index, bool formatLinkDownload)
        {
            try
            {
                var obj = new PlexMovie();
                LoggingHelpers.RecordGenericEntry(@"Content Parse Started");
                LoggingHelpers.RecordGenericEntry(@"Grabbing Titles");
                var metadata = XmlMetadataGatherers.GetContentMetadata(index);

                LoggingHelpers.RecordGenericEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.RecordGenericEntry(@"XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata, formatLinkDownload);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGenericEntry(@"Assembling Object");

                        obj.ContentGenre = XmlMetadataParsers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = XmlMetadataParsers.GetContentResolution(metadata);
                        obj.Actors = XmlMetadataParsers.GetActorsFromMetadata(metadata);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataParsers.GetContentSynopsis(metadata);
                    }
                    else
                    {
                        MessageBox.Show(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            @"DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            @"ContextDownloadInfoNull");
                        LoggingHelpers.RecordGenericEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGenericEntry("XML Invalid");
                }

                LoggingHelpers.RecordGenericEntry("Returned assembled Movie object");
                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Content metadata error:\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, @"MovieObjectError");
                return null;
            }
        }

        public static PlexMusic GetMusicObjectFromIndex(int index, bool formatLinkDownload)
        {
            try
            {
                var obj = new PlexMusic();
                LoggingHelpers.RecordGenericEntry(@"Content Parse Started");
                LoggingHelpers.RecordGenericEntry(@"Grabbing Titles");
                var metadata = XmlMetadataGatherers.GetTrackMetadata(index);

                LoggingHelpers.RecordGenericEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.RecordGenericEntry(@"XML Valid");

                    //MessageBox.Show(GlobalStaticVars.CurrentContentType.ToString());

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata, formatLinkDownload);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGenericEntry(@"Assembling Object");

                        obj.ContentGenre = XmlMetadataParsers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = new Structures.Resolution(); //audio doesn't have video-type resolution
                        obj.Actors = new System.Collections.Generic.List<PlexActor>(); //Plex audio does not contain the "<Role>" tag
                        obj.StreamIndex = index;
                        obj.Album = XmlMetadataParsers.GetParentTitle(metadata);
                        obj.Artist = XmlMetadataParsers.GetGrandparentTitle(metadata);
                        obj.Synopsis = "Auditory Content"; //Plex audio does not contain the "summary" attribute
                    }
                    else
                    {
                        MessageBox.Show(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            @"DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            @"ContextDownloadInfoNull");
                        LoggingHelpers.RecordGenericEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGenericEntry("XML Invalid");
                }

                LoggingHelpers.RecordGenericEntry("Returned assembled Music object");
                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Content metadata error:\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, @"MusicObjectError");
                return null;
            }
        }
    }
}