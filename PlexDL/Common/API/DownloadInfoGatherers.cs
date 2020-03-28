using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using System;
using System.Data;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class DownloadInfoGatherers
    {
        public static DownloadInfo GetContentDownloadInfo(XmlDocument xml)
        {
            try
            {
                if (Methods.PlexXmlValid(xml))
                {
                    var obj = new DownloadInfo();

                    LoggingHelpers.AddToLog("Grabbing DownloadInfo object");
                    var sections = new DataSet();
                    sections.ReadXml(new XmlNodeReader(xml));

                    var part = sections.Tables["Part"];
                    var video = sections.Tables["Video"].Rows[0];
                    var title = video["title"].ToString();
                    if (title.Length > GlobalStaticVars.Settings.Generic.DefaultStringLength)
                        title = title.Substring(0, GlobalStaticVars.Settings.Generic.DefaultStringLength);
                    var thumb = video["thumb"].ToString();
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

                    var link = GlobalStaticVars.GetBaseUri(false).TrimEnd('/') + filePart + "/?X-Plex-Token=" + GlobalStaticVars.GetToken();
                    obj.Link = link;
                    obj.Container = container;
                    obj.ByteLength = byteLength;
                    obj.ContentDuration = contentDuration;
                    obj.FileName = fileName;
                    obj.ContentTitle = title;
                    obj.ContentThumbnailUri = thumbnailFullUri;

                    return obj;
                }

                return new DownloadInfo();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "DownloadXmlError");
                return null;
            }
        }
    }
}