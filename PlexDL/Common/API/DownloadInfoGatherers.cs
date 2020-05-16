using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class DownloadInfoGatherers
    {
        public static DownloadInfo GetContentDownloadInfo(XmlDocument xml, bool formatLinkDownload)
        {
            try
            {
                if (Methods.PlexXmlValid(xml))
                {
                    var obj = new DownloadInfo();

                    LoggingHelpers.RecordGenericEntry("Grabbing DownloadInfo object");
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xml));

                    var part = sections.Tables["Part"];

                    DataRow dlRow = null;

                    //MessageBox.Show(GlobalStaticVars.CurrentContentType.ToString());

                    switch (GlobalStaticVars.CurrentContentType)
                    {
                        case ContentType.MOVIES:
                            dlRow = sections.Tables["Video"].Rows[0];
                            break;

                        case ContentType.TV_SHOWS:
                            dlRow = sections.Tables["Video"].Rows[0];
                            break;

                        case ContentType.MUSIC:
                            dlRow = sections.Tables["Track"].Rows[0];
                            break;
                    }

                    if (dlRow != null)
                    {
                        var title = dlRow["title"].ToString();
                        if (title.Length > GlobalStaticVars.Settings.Generic.DefaultStringLength)
                            title = title.Substring(0, GlobalStaticVars.Settings.Generic.DefaultStringLength);
                        var thumb = dlRow["thumb"].ToString();
                        var thumbnailFullUri = "";
                        if (string.IsNullOrEmpty(thumb))
                        {
                        }
                        else
                        {
                            var baseUri = GlobalStaticVars.GetBaseUri(false).TrimEnd('/');
                            thumbnailFullUri = baseUri + thumb + "?X-Plex-Token=" + GlobalStaticVars.GetToken();
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

                        var link = "";

                        //The PMS (Plex Media Server) will return the file as an octet-stream (download) if we set
                        //the GET parameter 'download' to '1' and a normal MP4 stream if we set it to '0'.
                        if (!formatLinkDownload)
                            link = GlobalStaticVars.GetBaseUri(false).TrimEnd('/') + filePart + "?download=0&X-Plex-Token=" + GlobalStaticVars.GetToken();
                        else
                            link = GlobalStaticVars.GetBaseUri(false).TrimEnd('/') + filePart + "?download=1&X-Plex-Token=" + GlobalStaticVars.GetToken();

                        obj.Link = link;
                        obj.Container = container;
                        obj.ByteLength = byteLength;
                        obj.ContentDuration = contentDuration;
                        obj.FileName = fileName;
                        obj.ContentTitle = title;
                        obj.ContentThumbnailUri = thumbnailFullUri;

                        return obj;
                    }
                    else
                        return new DownloadInfo();
                }

                return new DownloadInfo();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "DownloadXmlError");
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}