using PlexDL.Common.Caching;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common
{
    public static class Methods
    {
        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will return false.
                return false;
            }
        }

        public static bool IsPrivateIP(string ipAddress)
        {
            int[] ipParts = ipAddress.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => int.Parse(s)).ToArray();
            // in private ip range
            if (ipParts[0] == 10 ||
                (ipParts[0] == 192 && ipParts[1] == 168) ||
                (ipParts[0] == 172 && (ipParts[1] >= 16 && ipParts[1] <= 31)))
            {
                return true;
            }

            // IP Address is probably public.
            // This doesn't catch some VPN ranges like OpenVPN and Hamachi.
            return false;
        }

        public static string MatchUriToToken(string uri, List<Server> plexServers)
        {
            foreach (Server s in plexServers)
            {
                string serverUri = "http://" + s.address + ":" + s.port + "/";
                if (uri.Contains(serverUri))
                {
                    return s.accessToken;
                }
            }
            return "";
        }

        public static string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            string final = url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
            return final;
        }

        public static void SetHeaderText(DataGridView dgv, DataTable table)
        {
            //Copy column captions into DataGridView
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (table.Columns[col.Name].Caption != null)
                    col.HeaderText = table.Columns[col.Name].Caption;
            }
        }

        public static void SortingEnabled(DataGridView dgv, bool enabled)
        {
            if (enabled)
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            else
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public static bool PlexXmlValid(XmlDocument doc)
        {
            XmlNodeList checkNodes = doc.GetElementsByTagName("MediaContainer");

            if (checkNodes.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> OrderMatch(List<string> ordered, List<string> unordered)
        {
            List<string> newList = new List<string>();
            newList = unordered.OrderBy(d => ordered.IndexOf(d)).ToList();
            return newList;
        }

        public static PlexObject MetadataFromFile(string fileName)
        {
            try
            {
                PlexObject subReq = null;

                XmlSerializer serializer = new XmlSerializer(typeof(PlexObject));

                StreamReader reader = new StreamReader(fileName);
                subReq = (PlexObject)serializer.Deserialize(reader);
                reader.Close();

                return subReq;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred\n\n" + ex.ToString(), "Metadata Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.recordException(ex.Message, "XMLMetadataLoadError");
                return new PlexObject();
            }
        }

        public static string CalculateTime(double Time)
        {
            string mm, ss, CalculatedTime;
            int h, m, s, T;

            Time = System.Math.Round(Time) / 1000;
            T = System.Convert.ToInt32(Time);

            h = (T / 3600);
            T = T % 3600;
            m = (T / 60);
            s = T % 60;

            if (m < 10)
                mm = string.Format("0{0}", m);
            else
                mm = m.ToString();
            if (s < 10)
                ss = string.Format("0{0}", s);
            else
                ss = s.ToString();

            CalculatedTime = string.Format("{0}:{1}:{2}", h, mm, ss);

            return CalculatedTime;
        }

        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        public static Bitmap getImageFromUrl(string url)
        {
            try
            {
                Helpers.CacheStructureBuilder();
                if (url == "")
                {
                    return PlexDL.Properties.Resources.image_not_available_png_8;
                }
                else
                {
                    if (ThumbCaching.ThumbInCache(url))
                    {
                        return ThumbCaching.ThumbFromCache(url);
                    }
                    else
                    {
                        var request = System.Net.WebRequest.Create(url);

                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            Bitmap result = (Bitmap)Bitmap.FromStream(stream);
                            ThumbCaching.ThumbToCache(result, url);
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.recordException(ex.Message, "ImageFetchError");
                return PlexDL.Properties.Resources.image_not_available_png_8;
            }
        }

        public static string removeIllegalCharacters(string illegal)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(illegal, "");
        }
    }
}