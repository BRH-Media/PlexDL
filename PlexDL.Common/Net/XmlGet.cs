using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Extensions;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.WaitWindow;
using System;
using System.Threading;
using System.Xml;
using UIHelpers;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable InvertIf

namespace PlexDL.Common.Net
{
    public static class XmlGet
    {
        public static void GetXmlTransaction(object sender, WaitWindowEventArgs e)
        {
            //enforce correct argument count
            if (e.Arguments.Count > 0)
            {
                //first argument is always the URI to request
                var uri = (string)e.Arguments[0];

                //second argument is always whether or not to force a re-download
                var forceNoCache = false;

                //third argument is always whether or not to silence messages to the user
                var silent = false;

                //if specified, set forceNoCache
                if (e.Arguments.Count > 1)
                    forceNoCache = (bool)e.Arguments[1];

                //if specified, set silent
                if (e.Arguments.Count > 2)
                    silent = (bool)e.Arguments[2];

                //execute the download and return the result
                e.Result = GetXmlTransaction(uri, forceNoCache, silent, false);

                //exit method
                return;
            }

            //default
            e.Result = new XmlDocument();
        }

        public static XmlDocument GetXmlTransaction(string uri, bool forceNoCache = false,
            bool silent = false, bool waitWindow = true)
        {
            //allows multi-threaded operations
            if (waitWindow)

                //offload to another thread if specified
                return (XmlDocument)WaitWindow.WaitWindow.Show(GetXmlTransaction, @"Fetching from API", uri,
                    forceNoCache, silent);

            //Create the cache folder structure
            CachingHelpers.CacheStructureBuilder();

            //check if it's already cached
            if (XmlCaching.XmlInCache(uri) && !forceNoCache)
                try
                {
                    //load from the cache
                    var xmlCache = XmlCaching.XmlFromCache(uri);

                    //return the cached XML if not null, otherwise force a re-download
                    return xmlCache ?? GetXmlTransaction(uri, true, silent, false);
                }
                catch (Exception ex)
                {
                    //record the error
                    LoggingHelpers.RecordException(ex.Message, "CacheLoadError");

                    //force a re-download
                    return GetXmlTransaction(uri, true, silent, false);
                }

            //default secret account token
            var secret = !string.IsNullOrWhiteSpace(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken)
                ? ObjectProvider.Settings.ConnectionInfo.PlexAccountToken
                : ObjectProvider.User.authenticationToken;

            //allows specific server connection matching for the correct token
            var uriToken = string.IsNullOrEmpty(secret)
                ? Methods.MatchUriToToken(uri, ObjectProvider.PlexServers)
                : secret;

            //the API URI is combined with the token to yield the fully-qualified URI
            var fullUri = $@"{uri}{uriToken}";

            try
            {
                //get the resource
                var xmlString = ResourceGrab.GrabString(fullUri);

                //validation
                if (!string.IsNullOrWhiteSpace(xmlString))
                {
                    //conversion
                    var xmlResponse = xmlString.ToXmlDocument();

                    //log outcome
                    LoggingHelpers.RecordTransaction(fullUri, ResourceGrab.LastStatusCode);

                    //ensure file is cached
                    XmlCaching.XmlToCache(xmlResponse, uri);

                    //return XML document
                    return xmlResponse;
                }
            }
            catch (ThreadAbortException)
            {
                //literally nothing; this gets raised when a cancellation happens.
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XMLTransactionError");
                LoggingHelpers.RecordTransaction(fullUri, "Undetermined");
                if (!silent)
                    UIMessages.Error("Error Occurred in XML Transaction\n\n" + ex, @"Network Error");
            }

            //default
            return new XmlDocument();
        }
    }
}