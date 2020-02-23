using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlexDL.UI;
using System.Net;
using System.Xml;
using PlexDL.Common.Caching;
using PlexDL.Common;
using System.IO;
using System.Windows.Forms;
using PlexDL.Common.Logging;

namespace PlexDL.Common.API
{
    public class XmlGet
    {
        public static void GetXMLTransactionWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            string uri = (string)e.Arguments[0];
            e.Result = GetXMLTransaction(uri, Home.settings.ConnectionInfo.PlexAccountToken);
        }

        public static XmlDocument GetXMLTransaction(string uri, string secret = "", bool forceNoCache = false)
        {
            //Create the cache folder structure
            Helpers.CacheStructureBuilder();

            if (XMLCaching.XMLInCache(uri) && !forceNoCache)
            {
                XmlDocument XMLResponse = XMLCaching.XMLFromCache(uri);
                if (XMLResponse != null)
                {
                    return XMLResponse;
                }
                else
                {
                    return GetXMLTransaction(uri, "", true);
                }
            }
            else
            {
                //Declare XMLResponse document
                XmlDocument XMLResponse = null;
                //Declare an HTTP-specific implementation of the WebRequest class.
                HttpWebRequest objHttpWebRequest;
                //Declare an HTTP-specific implementation of the WebResponse class
                HttpWebResponse objHttpWebResponse = null;
                //Declare a generic view of a sequence of bytes
                Stream objResponseStream = null;
                //Declare XMLReader
                XmlTextReader objXMLReader;

                string secret2;
                if (secret == "")
                {
                    secret2 = Methods.MatchUriToToken(uri, Home.plexServers);
                }
                else
                {
                    secret2 = secret;
                }
                if (secret2 == "")
                {
                    secret2 = Home.settings.ConnectionInfo.PlexAccountToken;
                }
                string fullUri = uri + secret2;

                //6MessageBox.Show(fullUri);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //Creates an HttpWebRequest for the specified URL.
                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(fullUri);
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
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(objXMLReader);
                        //Set XMLResponse object returned from XMLReader
                        XMLResponse = xmldoc;
                        //Close XMLReader
                        objXMLReader.Close();
                    }
                    else if (objHttpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show("The web server denied access to the resource. Check your token and try again.");
                    }
                    //Close Steam
                    objResponseStream.Close();
                    //Close HttpWebResponse
                    objHttpWebResponse.Close();

                    LoggingHelpers.recordTransaction(fullUri, ((int)objHttpWebResponse.StatusCode).ToString());
                }
                catch (Exception ex)
                {
                    LoggingHelpers.recordException(ex.Message, "XMLTransactionError");
                    LoggingHelpers.recordTransaction(fullUri, "Undetermined");
                    MessageBox.Show("Error Occurred in XML Transaction\n\n" + ex.ToString(), "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new XmlDocument();
                }
                finally
                {
                    //Release objects
                    objXMLReader = null;
                    objResponseStream = null;
                    objHttpWebResponse = null;
                    objHttpWebRequest = null;
                }
                XMLCaching.XMLToCache(XMLResponse, uri);
                return XMLResponse;
            }
        }
    }
}
