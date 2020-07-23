using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.WaitWindow;
using System;
using System.IO;
using System.Net;
using System.Xml;
using UIHelpers;

namespace PlexDL.Common.API
{
    public static class XmlGet
    {
        public static void GetXmlTransactionWorker(object sender, WaitWindowEventArgs e)
        {
            var uri = (string)e.Arguments[0];
            var forceNoCache = false;
            var silent = false;
            if (e.Arguments.Count > 1)
                forceNoCache = (bool)e.Arguments[1];

            if (e.Arguments.Count > 2)
                silent = (bool)e.Arguments[2];

            e.Result = GetXmlTransaction(uri, ObjectProvider.Settings.ConnectionInfo.PlexAccountToken, forceNoCache, silent);
        }

        public static XmlDocument GetXmlTransaction(string uri, string secret = "", bool forceNoCache = false, bool silent = false)
        {
            //Create the cache folder structure
            Helpers.CacheStructureBuilder();

            if (XmlCaching.XmlInCache(uri) && !forceNoCache)
                try
                {
                    var xmlCache = XmlCaching.XmlFromCache(uri);
                    return xmlCache ?? GetXmlTransaction(uri, "", true);
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "CacheLoadError");
                    //force the GetXMLTransaction method to ignore cached items
                    return GetXmlTransaction(uri, secret, true, silent);
                }

            //Declare XMLResponse document
            XmlDocument xmlResponse = null;
            //Declare an HTTP-specific implementation of the WebRequest class.
            //Declare a generic view of a sequence of bytes
            Stream objResponseStream = null;
            //Declare XMLReader

            var secret2 = string.IsNullOrEmpty(secret) ? Methods.MatchUriToToken(uri, ObjectProvider.PlexServers) : secret;
            if (string.IsNullOrEmpty(secret2))
                secret2 = ObjectProvider.Settings.ConnectionInfo.PlexAccountToken;

            var fullUri = uri + secret2;

            //UIMessages.Info(fullUri);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //Creates an HttpWebRequest for the specified URL.
            var objHttpWebRequest = (HttpWebRequest)WebRequest.Create(fullUri);
            //Declare an HTTP-specific implementation of the WebResponse class
            //---------- Start HttpRequest
            try
            {
                //Set HttpWebRequest properties
                objHttpWebRequest.Method = "GET";
                objHttpWebRequest.KeepAlive = false;
                //---------- End HttpRequest
                //Sends the HttpWebRequest, and waits for a response.
                var objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                switch (objHttpWebResponse.StatusCode)
                {
                    //---------- Start HttpResponse, Return code 200
                    case HttpStatusCode.OK:
                        {
                            objResponseStream = objHttpWebResponse.GetResponseStream();
                            xmlResponse = objResponseStream.ToXmlDocument();
                            break;
                        }
                    case HttpStatusCode.Unauthorized:
                        {
                            if (!silent)
                                UIMessages.Error("The web server denied access to the resource. Check your token and try again.");
                            break;
                        }
                }

                //Close Steam
                objResponseStream?.Close();
                //Close HttpWebResponse
                objHttpWebResponse.Close();

                LoggingHelpers.RecordTransaction(fullUri, ((int)objHttpWebResponse.StatusCode).ToString());
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XMLTransactionError");
                LoggingHelpers.RecordTransaction(fullUri, "Undetermined");
                if (!silent)
                    UIMessages.Error("Error Occurred in XML Transaction\n\n" + ex, @"Network Error");
                return new XmlDocument();
            }

            XmlCaching.XmlToCache(xmlResponse, uri);
            return xmlResponse;
        }

        public static XmlDocument ToXmlDocument(this XmlTextReader reader)
        {
            //Declare XMLDocument
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            //Set XMLResponse object returned from XMLReader
            var val = xmlDoc;
            //Close XMLReader
            reader.Close();
            return val;
        }

        public static XmlDocument ToXmlDocument(this Stream dataStream)
        {
            //Load stream into XMLReader
            var objXmlReader = new XmlTextReader(dataStream);

            return objXmlReader.ToXmlDocument(); //using the method above
        }

        public static XmlDocument ToXmlDocument(this string xmlString)
        {
            //Load string into XmlReader
            var objXmlReader = new XmlTextReader(xmlString);

            return objXmlReader.ToXmlDocument(); //using the method above
        }
    }
}