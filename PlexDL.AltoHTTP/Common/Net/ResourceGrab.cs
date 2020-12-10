using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PlexDL.AltoHTTP.Common.Net
{
    /// <summary>
    /// Web client handler; processes web resources for download
    /// </summary>
    public static class ResourceGrab
    {
        /// <summary>
        /// Stores the most recent HTTP response status code
        /// </summary>
        public static string LastStatusCode { get; set; } = @"";

        /// <summary>
        /// Downloads a web resource then transforms it into an ASCII/UTF-8 string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GrabString(Uri uri, string referrer = @"", string method = @"GET")
            => GrabString(uri.ToString(), referrer, method);

        /// <summary>
        /// Downloads a web resource then transforms it into an ASCII/UTF-8 string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GrabString(string uri, string referrer = @"", string method = @"GET")
        {
            //download raw bytes
            var data = GrabBytes(uri, referrer, method);

            //validation; convert bytes to string then return the result
            return data != null
                ? Encoding.Default.GetString(data)
                : @"";
        }

        /// <summary>
        /// Downloads a raw web resource with no transforms
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static byte[] GrabBytes(Uri uri, string referrer = @"", string method = @"GET")
            => GrabBytes(uri.ToString(), referrer, method);

        /// <summary>
        /// Downloads a raw web resource with no transforms
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static byte[] GrabBytes(string uri, string referrer = @"", string method = @"GET")
        {
            //request handler
            var handler = new HttpClientHandler
            {
                //allows decompression methods
                AutomaticDecompression = ~DecompressionMethods.None,

                //complies with 'Location' headers
                AllowAutoRedirect = true,

                //supports settings cookies
                CookieContainer = new CookieContainer(),
            };

            //request client
            var client = new HttpClient(handler);

            //apply timeout
            if (NetGlobals.Timeout > 0)
                client.Timeout = TimeSpan.FromSeconds(NetGlobals.Timeout);

            //add user agent
            client.DefaultRequestHeaders.Add(@"User-Agent", NetGlobals.GlobalUserAgent);

            //new request message for this session
            var request = new HttpRequestMessage(new HttpMethod(method), uri);

            //if the referrer was set, we can assign the header a value
            if (!string.IsNullOrWhiteSpace(referrer))
                request.Headers.TryAddWithoutValidation("Referer", referrer);

            //the response from the server is retrieved using the client
            var response = client.SendAsync(request).Result;

            //raw response body (in bytes)
            var body = response.Content.ReadAsByteArrayAsync().Result;

            //set the status code global
            LastStatusCode = ((int)response.StatusCode).ToString();

            //verify status code
            if (string.IsNullOrWhiteSpace(LastStatusCode))
                LastStatusCode = @"Unknown";

            //return response body
            return body;
        }
    }
}