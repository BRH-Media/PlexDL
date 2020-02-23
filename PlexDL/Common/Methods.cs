using PlexDL.Common.Caching;
using PlexDL.Common.Structures;
using PlexDL.UI;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common
{
    public static class Methods
    {
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
                Home.recordException(ex.Message, "XMLMetadataLoadError");
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
                Home.recordException(ex.Message, "ImageFetchError");
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