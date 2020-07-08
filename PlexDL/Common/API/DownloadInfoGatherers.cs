using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using System;
using System.Data;
using System.Xml;
using PlexDL.Common.Globals.Providers;
using UIHelpers;

namespace PlexDL.Common.API
{
    public static class DownloadInfoGatherers
    {
        public static DownloadInfo GetContentDownloadInfo(XmlDocument xml, bool formatLinkDownload)
        {
            try
            {
                if (!Methods.PlexXmlValid(xml)) return new DownloadInfo();

                var obj = new DownloadInfo();

                LoggingHelpers.RecordGeneralEntry("Grabbing DownloadInfo object");
                var sections = new DataSet();
                sections.ReadXml(new XmlNodeReader(xml));

                var part = sections.Tables["Part"];

                DataRow dlRow = null;

                //UIMessages.Info(ObjectProvider.CurrentContentType.ToString());

                switch (ObjectProvider.CurrentContentType)
                {
                    case ContentType.Movies:
                        dlRow = sections.Tables["Video"].Rows[0];
                        break;

                    case ContentType.TvShows:
                        dlRow = sections.Tables["Video"].Rows[0];
                        break;

                    case ContentType.Music:
                        dlRow = sections.Tables["Track"].Rows[0];
                        break;
                }

                if (dlRow == null) return new DownloadInfo();

                var title = dlRow["title"].ToString();
                if (title.Length > ObjectProvider.Settings.Generic.DefaultStringLength)
                    title = title.Substring(0, ObjectProvider.Settings.Generic.DefaultStringLength);
                var thumb = dlRow["thumb"].ToString();
                var thumbnailFullUri = "";
                if (string.IsNullOrEmpty(thumb))
                {
                }
                else
                {
                    var baseUri = Strings.GetBaseUri(false).TrimEnd('/');
                    thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + Strings.GetToken();
                }

                var partRow = part.Rows[0];

                var filePart = "";
                var container = "";
                if (partRow["key"] != null)
                    filePart = partRow["key"].ToString();
                if (partRow["container"] != null)
                    container = partRow["container"].ToString();
                var byteLength = Convert.ToInt64(partRow["size"]);
                var contentDuration = Convert.ToInt32(partRow["duration"]);
                var fileName = Methods.RemoveIllegalCharacters(title + "." + container);

                string link;

                //The PMS (Plex Media Server) will return the file as an octet-stream (download) if we set
                //the GET parameter 'download' to '1' and a normal MP4 stream if we set it to '0'.
                if (!formatLinkDownload)
                    link = Strings.GetBaseUri(false).TrimEnd('/') + filePart + "?download=0&X-Plex-Token=" + Strings.GetToken();
                else
                    link = Strings.GetBaseUri(false).TrimEnd('/') + filePart + "?download=1&X-Plex-Token=" + Strings.GetToken();

                obj.Link = link;
                obj.Container = container;
                obj.ByteLength = byteLength;
                obj.ContentDuration = contentDuration;
                obj.FileName = fileName;
                obj.ContentTitle = title;
                obj.ContentThumbnailUri = thumbnailFullUri;

                return obj;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "DownloadXmlError");
                UIMessages.Error(ex.ToString());
                return null;
            }
        }
    }
}