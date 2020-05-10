using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;
using PlexDL.Properties;
using PlexDL.WaitWindow;
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

namespace PlexDL.Common
{
    public static class Methods
    {
        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                var request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                var response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                //Any exception will return false.
                return false;
            }
        }

        public static bool IsPrivateIp(string ipAddress)
        {
            var ipParts = ipAddress.Split(new[]
            {
                "."
            }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
            // in private ip range
            if (ipParts[0] == 10 ||
                ipParts[0] == 192 && ipParts[1] == 168 ||
                ipParts[0] == 172 && ipParts[1] >= 16 && ipParts[1] <= 31)
                return true;

            // IP Address is probably public.
            // This doesn't catch some VPN ranges like OpenVPN and Hamachi.
            return false;
        }

        public static string MatchUriToToken(string uri, List<Server> plexServers)
        {
            foreach (var s in plexServers)
            {
                var serverUri = "http://" + s.address + ":" + s.port + "/";
                if (uri.Contains(serverUri))
                    return s.accessToken;
            }

            return "";
        }

        public static string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            var final = url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
            return final;
        }

        public static void SetHeaderText(DataGridView dgv, DataTable table)
        {
            //Copy column captions into DataGridView
            foreach (DataGridViewColumn col in dgv.Columns)
                if (table.Columns[col.Name].Caption != null)
                    col.HeaderText = table.Columns[col.Name].Caption;
        }

        public static void SortingEnabled(DataGridView dgv, bool enabled)
        {
            if (enabled)
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.Automatic;
            else
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public static bool PlexXmlValid(XmlDocument doc)
        {
            var checkNodes = doc.GetElementsByTagName("MediaContainer");

            if (checkNodes.Count > 0)
                return true;
            return false;
        }

        public static List<string> OrderMatch(List<string> ordered, List<string> unordered)
        {
            var newList = unordered.OrderBy(d => ordered.IndexOf(d)).ToList();
            return newList;
        }

        public static string CalculateTime(double Time)
        {
            string mm, ss, CalculatedTime;
            int h, m, s, T;

            //convert back to seconds from miliseconds
            Time = Math.Round(Time) / 1000;
            T = Convert.ToInt32(Time);

            h = T / 3600;
            T %= 3600;
            m = T / 60;
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

        public static bool WebSiteCheckMT(string uri)
        {
            var args = new object[] { uri };
            var available = (bool)WaitWindow.WaitWindow.Show(Methods.CheckWebWorker, "Checking Connection", args);
            return available;
        }

        private static void CheckWebWorker(object sender, WaitWindowEventArgs e)
        {
            var uri = (string)e.Arguments[0];
            //check with no wait-window. It's called WebSiteCheckST because of single-threaded...
            e.Result = WebSiteCheckST(uri);
        }

        public static bool WebSiteCheckST(string Url)
        {
            var Message = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            //the request will timeout after 4.5 seconds
            request.Timeout = 4500;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += (Message.Length > 0 ? "\n" : "") + ex.Message;
            }

            return Message.Length == 0;
        }

        public static string GenerateRandomNumber(int length)
        {
            Random random = new Random();
            string r = "";

            for (int i = 1; i < length + 1; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }

        public static string FormatBytes(long bytes, bool includeSpace = false)
        {
            string[] Suffix =
            {
                "B", "KB", "MB", "GB", "TB"
            };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            if (includeSpace)
                return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
            else
                return string.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }

        public static bool AdultKeywordCheck(PlexObject stream)
        {
            var keywords = Resources.keywordBlacklist.Split('\n');
            var search = stream.StreamInformation.ContentTitle.ToLower();
            var rgx = new Regex("[^a-zA-Z0-9_. ]+");
            //MessageBox.Show(keywords.Length.ToString());
            search = rgx.Replace(search, "");
            var match = false;
            if (string.Equals(stream.ContentGenre.ToLower(), "porn") || string.Equals(stream.ContentGenre.ToLower(), "adult"))
            {
                match = true;
            }
            else
            {
                if (keywords.Length > 0)
                    foreach (var k in keywords)
                        if (!string.IsNullOrEmpty(k) && !string.IsNullOrWhiteSpace(k))
                        {
                            var clean = rgx.Replace(k, "").ToLower();
                            if (search.Contains(clean))
                            {
                                match = true;
                                break;
                            }
                        }
            }

            return match;
        }

        public static bool StreamAdultContentCheck(PlexObject stream)
        {
            if (GlobalStaticVars.Settings.Generic.AdultContentProtection)
                //just to keep things family-friendly, show a warning for possibly adult-type content :)
                if (AdultKeywordCheck(stream))
                {
                    var result =
                    MessageBox.Show("The content you're about to view may contain adult (18+) themes. Are you okay with viewing this content?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.No)
                        return false;
                }

            return true;
        }

        public static Bitmap GetImageFromUrl(string url, bool forceNoCache = false)
        {
            try
            {
                Helpers.CacheStructureBuilder();
                if (string.IsNullOrEmpty(url))
                    return Resources.image_not_available_png_8;

                if (!forceNoCache)
                    if (ThumbCaching.ThumbInCache(url))
                        return ThumbCaching.ThumbFromCache(url);
            }
            catch (UnauthorizedAccessException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ThumbIOAccessError");
                return ForceImageFromUrl(url);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ImageFetchError");
                return Resources.image_not_available_png_8;
            }

            return ForceImageFromUrl(url);
        }

        private static Bitmap ForceImageFromUrl(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var result = (Bitmap)Image.FromStream(stream);
                    ThumbCaching.ThumbToCache(result, url);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ImageFetchError");
                return Resources.image_not_available_png_8;
            }
        }

        public static bool ValidateIPv4(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
                return false;

            var splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
                return false;

            return splitValues.All(r => byte.TryParse(r, out var tempForParsing));
        }

        public static string RemoveIllegalCharacters(string illegal)
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex($"[{Regex.Escape(regexSearch)}]");
            return r.Replace(illegal, "");
        }
    }
}