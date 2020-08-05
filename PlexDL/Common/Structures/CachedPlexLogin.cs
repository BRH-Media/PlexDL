using PlexDL.Common.Logging;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures
{
    public class CachedPlexLogin
    {
        //for just making the object; no arguments needed.
        public CachedPlexLogin()
        {
            //blank
        }

        //for specifying credentials then calculating everything else
        public CachedPlexLogin(string un, string pw)
        {
            var dt = DateTime.Now.ToString();
            var content = un + "\n" + pw + "\n" + dt;
            var hash = MD5Helper.CalculateMd5Hash(content);

            //set the object values
            Username = un;
            Password = pw;
            WriteDateTime = dt;
            MD5Checksum = hash;
        }

        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string WriteDateTime { get; set; } = "";
        public string MD5Checksum { get; set; } = "";

        public void ToFile(string fileName = "plex.account")
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(CachedPlexLogin));
                var xmlSettings = new XmlWriterSettings();
                var sww = new StringWriter();
                xmlSettings.Indent = true;
                xmlSettings.IndentChars = "\t";
                xmlSettings.OmitXmlDeclaration = false;

                xsSubmit.Serialize(sww, this);

                File.WriteAllText(fileName, sww.ToString());

                LoggingHelpers.RecordGeneralEntry("Saved PlexLogin data to '" + fileName + "'");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "PlexLoginSaveError");
            }
        }

        public bool VerifyThis()
        {
            try
            {
                var content = Username + "\n" + Password + "\n" + WriteDateTime;
                var actualHash = MD5Helper.CalculateMd5Hash(content);
                var storedHash = MD5Checksum;
                return string.Equals(storedHash, actualHash);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "PlexLoginVerifyError");
                return false;
            }
        }

        public static CachedPlexLogin FromFile(string fileName = "plex.account")
        {
            try
            {
                CachedPlexLogin subReq;

                var serializer = new XmlSerializer(typeof(CachedPlexLogin));

                var reader = new StreamReader(fileName);
                subReq = (CachedPlexLogin)serializer.Deserialize(reader);
                reader.Close();
                return subReq;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "PlexLoginLoadError");
                return new CachedPlexLogin();
            }
        }
    }
}