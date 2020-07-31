using PlexDL.Common.API.Metadata.Handlers;
using PlexDL.Common.API.Metadata.Handlers.Parsers;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using UIHelpers;

namespace PlexDL.Common.API.Objects
{
    public static class ObjectBuilders
    {
        public static PlexTvShow GetTvObjectFromIndex(int index)
        {
            try
            {
                var obj = new PlexTvShow();
                LoggingHelpers.RecordGeneralEntry(@"Content Parse Started");
                LoggingHelpers.RecordGeneralEntry(@"Grabbing Titles");

                var metadata = XmlMetadataContent.GetEpisodeMetadata(index);

                LoggingHelpers.RecordGeneralEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata.Xml))
                {
                    LoggingHelpers.RecordGeneralEntry(@"XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata.Xml);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGeneralEntry(@"Assembling Object");

                        obj.ApiUri = metadata.ApiUri;
                        obj.ContentGenre = XmlMetadataStrings.GetContentGenre(metadata.Xml);
                        obj.StreamInformation = dlInfo;
                        obj.Season = XmlMetadataStrings.GetParentTitle(metadata.Xml);
                        obj.EpisodesInSeason = TableProvider.EpisodesTable.Rows.Count;
                        //this is in 0-based format. This means the lowest number is 0 instead of 1.
                        //we need to display "Episode 1" instead of "Episode 0" for the first episode,
                        //so bump the index by 1.
                        obj.EpisodeNumber = index + 1;
                        obj.TvShowName = XmlMetadataStrings.GetGrandparentTitle(metadata.Xml);
                        obj.StreamResolution = XmlMetadataObjects.GetContentResolution(metadata.Xml);
                        obj.Actors = XmlMetadataObjects.GetActorsFromMetadata(metadata.Xml);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataStrings.GetContentSynopsis(metadata.Xml);
                    }
                    else
                    {
                        UIMessages.Error(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error");
                        LoggingHelpers.RecordException(
                            "DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            "ContextDownloadInfoNull");
                        LoggingHelpers.RecordGeneralEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGeneralEntry("XML Invalid");
                }

                LoggingHelpers.RecordGeneralEntry("Returned assembled TV object");
                return obj;
            }
            catch (Exception ex)
            {
                UIMessages.Error("Content metadata error:\n\n" + ex, @"Data Error");
                LoggingHelpers.RecordException(ex.Message, "TVObjectError");
                return null;
            }
        }

        public static PlexMovie GetMovieObjectFromIndex(int index)
        {
            try
            {
                var obj = new PlexMovie();
                LoggingHelpers.RecordGeneralEntry(@"Content Parse Started");
                LoggingHelpers.RecordGeneralEntry(@"Grabbing Titles");
                var metadata = XmlMetadataContent.GetContentMetadata(index);

                LoggingHelpers.RecordGeneralEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata.Xml))
                {
                    LoggingHelpers.RecordGeneralEntry(@"XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata.Xml);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGeneralEntry(@"Assembling Object");

                        obj.ApiUri = metadata.ApiUri;
                        obj.ContentGenre = XmlMetadataStrings.GetContentGenre(metadata.Xml);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = XmlMetadataObjects.GetContentResolution(metadata.Xml);
                        obj.Actors = XmlMetadataObjects.GetActorsFromMetadata(metadata.Xml);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataStrings.GetContentSynopsis(metadata.Xml);
                    }
                    else
                    {
                        UIMessages.Error(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error");
                        LoggingHelpers.RecordException(
                            @"DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            @"ContextDownloadInfoNull");
                        LoggingHelpers.RecordGeneralEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGeneralEntry("XML Invalid");
                }

                LoggingHelpers.RecordGeneralEntry("Returned assembled Movie object");
                return obj;
            }
            catch (Exception ex)
            {
                UIMessages.Error("Content metadata error:\n\n" + ex, @"Data Error");
                LoggingHelpers.RecordException(ex.Message, @"MovieObjectError");
                return null;
            }
        }

        public static PlexMusic GetMusicObjectFromIndex(int index)
        {
            try
            {
                var obj = new PlexMusic();
                LoggingHelpers.RecordGeneralEntry(@"Content Parse Started");
                LoggingHelpers.RecordGeneralEntry(@"Grabbing Titles");
                var metadata = XmlMetadataContent.GetTrackMetadata(index);

                LoggingHelpers.RecordGeneralEntry(@"Checking XML validity");
                if (Methods.PlexXmlValid(metadata.Xml))
                {
                    LoggingHelpers.RecordGeneralEntry(@"XML Valid");

                    //UIMessages.Info(ObjectProvider.CurrentContentType.ToString());

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo(metadata.Xml);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.RecordGeneralEntry(@"Assembling Object");

                        obj.ApiUri = metadata.ApiUri;
                        obj.ContentGenre = XmlMetadataStrings.GetContentGenre(metadata.Xml);
                        obj.StreamInformation = dlInfo;
                        obj.StreamResolution = new Structures.Resolution(); //audio doesn't have video-type resolution
                        obj.Actors = new System.Collections.Generic.List<PlexActor>(); //Plex audio does not contain the "<Role>" tag
                        obj.StreamIndex = index;
                        obj.Album = XmlMetadataStrings.GetParentTitle(metadata.Xml);
                        obj.Artist = XmlMetadataStrings.GetGrandparentTitle(metadata.Xml);
                        obj.Synopsis = "Auditory Content"; //Plex audio does not contain the "summary" attribute
                    }
                    else
                    {
                        UIMessages.Error(
                            @"Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            @"Data Error");
                        LoggingHelpers.RecordException(
                            @"DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            @"ContextDownloadInfoNull");
                        LoggingHelpers.RecordGeneralEntry("DownloadInfo is invalid (no stream contextual information)");
                    }
                }
                else
                {
                    LoggingHelpers.RecordGeneralEntry("XML Invalid");
                }

                LoggingHelpers.RecordGeneralEntry("Returned assembled Music object");
                return obj;
            }
            catch (Exception ex)
            {
                UIMessages.Error("Content metadata error:\n\n" + ex, @"Data Error");
                LoggingHelpers.RecordException(ex.Message, @"MusicObjectError");
                return null;
            }
        }
    }
}