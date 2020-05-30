using PlexDL.Common.API.Metadata;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Windows.Forms;

namespace PlexDL.Common.API.Objects
{
    public static class ObjectBuilders
    {
        public static PlexTvShow GetTvObjectFromIndex(int index, bool formatLinkDownload)
        {
            try
            {
                var obj = new PlexTvShow();
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
                        //this is in 0-based format. This means the lowest number is 0 instead of 1.
                        //we need to display "Episode 1" instead of "Episode 0" for the first episode,
                        //so bump the index by 1.
                        obj.EpisodeNumber = index + 1;
                        obj.TvShowName = XmlMetadataParsers.GetGrandparentTitle(metadata);
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