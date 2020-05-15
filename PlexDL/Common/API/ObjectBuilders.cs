using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Windows.Forms;

namespace PlexDL.Common.API
{
    public static class ObjectBuilders
    {
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

                        obj.ContentGenre = XmlMetadataGatherers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.Season = XmlMetadataGatherers.GetParentTitle(metadata);
                        obj.EpisodesInSeason = GlobalTables.EpisodesTable.Rows.Count;
                        obj.TVShowName = XmlMetadataGatherers.GetGrandparentTitle(metadata);
                        obj.StreamResolution = XmlMetadataGatherers.GetContentResolution(metadata);
                        obj.Actors = XmlMetadataGatherers.GetActorsFromMetadata(metadata);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataGatherers.GetContentSynopsis(metadata);
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

                        obj.ContentGenre = XmlMetadataGatherers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = XmlMetadataGatherers.GetContentResolution(metadata);
                        obj.Actors = XmlMetadataGatherers.GetActorsFromMetadata(metadata);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataGatherers.GetContentSynopsis(metadata);
                    }
                    else
                    {
                        MessageBox.Show(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            @"DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            @"ContextDownloadInfoNull");
                        LoggingHelpers.RecordGenericEntry("DownloadInfo is invalid (no stream contexual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGenericEntry("XML Invalid");
                }

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
                var metadata = XmlMetadataGatherers.GetContentMetadata(index);

                LoggingHelpers.RecordGenericEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.RecordGenericEntry(@"XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata, formatLinkDownload);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGenericEntry(@"Assembling Object");

                        obj.ContentGenre = XmlMetadataGatherers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = new Structures.Resolution(); //audio doesn't have video-type resolution
                        obj.Actors = new System.Collections.Generic.List<PlexActor>(); //Plex audio does not contain the "<Role>" tag
                        obj.StreamIndex = index;
                        obj.Album = XmlMetadataGatherers.GetParentTitle(metadata);
                        obj.Artist = XmlMetadataGatherers.GetGrandparentTitle(metadata);
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
                        LoggingHelpers.RecordGenericEntry("DownloadInfo is invalid (no stream contexual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGenericEntry("XML Invalid");
                }

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