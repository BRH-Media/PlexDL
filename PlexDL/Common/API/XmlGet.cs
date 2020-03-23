﻿using PlexDL.Common.Caching;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class XmlGet
    {
        public static void GetXMLTransactionWorker(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var uri = (string)e.Arguments[0];
            var forceNoCache = false;
            var silent = false;
            if (e.Arguments.Count > 1)
                forceNoCache = (bool)e.Arguments[1];

            if (e.Arguments.Count > 2)
                silent = (bool)e.Arguments[2];

            e.Result = GetXMLTransaction(uri, GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken, forceNoCache, silent);
        }

        public static XmlDocument GetXMLTransaction(string uri, string secret = "", bool forceNoCache = false, bool silent = false)
        {
            //Create the cache folder structure
            Helpers.CacheStructureBuilder();

            if (XMLCaching.XMLInCache(uri) && !forceNoCache)
            {
                try
                {
                    var XMLResponse = XMLCaching.XMLFromCache(uri);
                    if (XMLResponse != null)
                        return XMLResponse;
                    else
                        return GetXMLTransaction(uri, "", true);
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "CacheLoadError");
                    //force the GetXMLTransaction method to ignore cached items
                    return GetXMLTransaction(uri, secret, true, silent);
                }
            }
            else
            {
                //Declare XMLResponse document
                XmlDocument XMLResponse = null;
                //Declare an HTTP-specific implementation of the WebRequest class.
                HttpWebRequest objHttpWebRequest;
                //Declare a generic view of a sequence of bytes
                Stream objResponseStream = null;
                //Declare XMLReader
                XmlTextReader objXMLReader;

                var secret2 = string.IsNullOrEmpty(secret) ? Methods.MatchUriToToken(uri, GlobalStaticVars.PlexServers) : secret;
                if (string.IsNullOrEmpty(secret2))
                    secret2 = GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken;

                var fullUri = uri + secret2;

                //6MessageBox.Show(fullUri);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //Creates an HttpWebRequest for the specified URL.
                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(fullUri);
                //Declare an HTTP-specific implementation of the WebResponse class
                HttpWebResponse objHttpWebResponse;
                //---------- Start HttpRequest
                try
                {
                    //Set HttpWebRequest properties
                    objHttpWebRequest.Method = "GET";
                    objHttpWebRequest.KeepAlive = false;
                    //---------- End HttpRequest
                    //Sends the HttpWebRequest, and waits for a response.
                    objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                    //---------- Start HttpResponse, Return code 200
                    if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //Get response stream
                        objResponseStream = objHttpWebResponse.GetResponseStream();
                        //Load response stream into XMLReader
                        objXMLReader = new XmlTextReader(objResponseStream);
                        //Declare XMLDocument
                        var xmldoc = new XmlDocument();
                        xmldoc.Load(objXMLReader);
                        //Set XMLResponse object returned from XMLReader
                        XMLResponse = xmldoc;
                        //Close XMLReader
                        objXMLReader.Close();
                    }
                    else if (objHttpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (!silent)
                            MessageBox.Show("The web server denied access to the resource. Check your token and try again.");
                    }

                    //Close Steam
                    objResponseStream.Close();
                    //Close HttpWebResponse
                    objHttpWebResponse.Close();

                    LoggingHelpers.RecordTransaction(fullUri, ((int)objHttpWebResponse.StatusCode).ToString());
                }
                catch (Exception ex)
                {
                    LoggingHelpers.RecordException(ex.Message, "XMLTransactionError");
                    LoggingHelpers.RecordTransaction(fullUri, "Undetermined");
                    if (!silent)
                        MessageBox.Show("Error Occurred in XML Transaction\n\n" + ex.ToString(), "Network Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return new XmlDocument();
                }

                XMLCaching.XMLToCache(XMLResponse, uri);
                return XMLResponse;
            }
        }
    }
}