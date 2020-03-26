using System;
using System.Windows.Forms;
using System.Xml;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;

namespace PlexDL.Common.API
{
    public static class ObjectBuilders
    {
        public static PlexTVShow GetTVObjectFromIndex(int index)
        {
            try
            {
                var obj = new PlexTVShow();
                XmlDocument metadata;
                LoggingHelpers.AddToLog("Content Parse Started");
                LoggingHelpers.AddToLog("Grabbing Titles");

                metadata = XmlMetadataGatherers.GetEpisodeMetadata(index);

                LoggingHelpers.AddToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.AddToLog("XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo_Xml(metadata);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.AddToLog("Assembling Object");

                        obj.ContentGenre = XmlMetadataGatherers.GetContentGenre(metadata);
                        obj.StreamInformation = dlInfo;
                        obj.Season = XmlMetadataGatherers.GetTVShowSeason(metadata);
                        obj.EpisodesInSeason = GlobalTables.EpisodesTable.Rows.Count;
                        obj.TVShowName = XmlMetadataGatherers.GetTVShowTitle(metadata);
                        obj.StreamResolution = XmlMetadataGatherers.GetContentResolution(metadata);
                        obj.Actors = XmlMetadataGatherers.GetActorsFromMetadata(metadata);
                        obj.StreamIndex = index;
                        obj.Synopsis = XmlMetadataGatherers.GetContentSynopsis(metadata);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            "DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            "ContextDownloadInfoNull");
                        LoggingHelpers.AddToLog("DownloadInfo is invalid (no stream contexual information)");
                    }
                }
                else
                {
                    LoggingHelpers.AddToLog("XML Invalid");
                }

                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Content metadata error:\n\n" + ex, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "TVObjectError");
                return null;
            }
        }

        public static PlexMovie GetMovieObjectFromIndex(int index)
        {
            try
            {
                var obj = new PlexMovie();
                XmlDocument metadata;
                LoggingHelpers.AddToLog("Content Parse Started");
                LoggingHelpers.AddToLog("Grabbing Titles");
                metadata = XmlMetadataGatherers.GetContentMetadata(index);

                LoggingHelpers.AddToLog("Checking XML validity");
                if (Methods.PlexXmlValid(metadata))
                {
                    LoggingHelpers.AddToLog("XML Valid");

                    var dlInfo = DownloadInfoGatherers.GetContentDownloadInfo_Xml(metadata);

                    if (dlInfo != null)
                    {
                        LoggingHelpers.AddToLog("Assembling Object");

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
                            "Failed to get contextual information; an unknown error occurred. Check the exception log for more information.",
                            "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoggingHelpers.RecordException(
                            "DownloadInfo invalid. This may be an internal error; please report this issue on GitHub.",
                            "ContextDownloadInfoNull");
                        LoggingHelpers.AddToLog("DownloadInfo is invalid (no stream contexual information)");
                    }
                }
                else
                {
                    LoggingHelpers.AddToLog("XML Invalid");
                }

                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Content metadata error:\n\n" + ex, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "MovieObjectError");
                return null;
            }
        }
    }
}