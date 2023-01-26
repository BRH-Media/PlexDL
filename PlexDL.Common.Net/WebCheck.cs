using System;
using System.Net;
using PlexDL.WaitWindow;

namespace PlexDL.Common.Net
{
    public class WebCheck
    {
        public Exception LastException { get; set; }
        public string StatusCode { get; set; } = "Undetermined";
        public bool ConnectionSuccess { get; set; }

        private static void TestUrl(object sender, WaitWindowEventArgs e)
        {
            var url = (string)e.Arguments[0];
            e.Result = TestUrl(url, false);
        }

        public static WebCheck TestUrl(string url, bool waitWindow = true)
        {
            if (waitWindow)
                return (WebCheck)WaitWindow.WaitWindow.Show(TestUrl, @"Checking connection", url);

            var obj = new WebCheck();

            try
            {
                //Creating the HttpWebRequest
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = NetGlobals.GlobalUserAgent;

                //5s timeout
                request.Timeout = 5000;

                //don't try and request the full page!
                request.Method = "HEAD";

                //Getting the Web Response.
                var response = (HttpWebResponse)request.GetResponse();

                //Release the stream
                response.Close();

                //Returns TRUE if the Status code == 200
                obj.ConnectionSuccess = response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(WebException))
                {
                    var webEx = (WebException)ex;
                    var status = webEx.Status;
                    if (status == WebExceptionStatus.ProtocolError)
                    {
                        var response = (HttpWebResponse)webEx.Response;
                        if (response != null)
                            obj.StatusCode = response.StatusCode.ToString();
                    }
                }

                obj.LastException = ex;
                obj.ConnectionSuccess = false;
            }

            return obj;
        }
    }
}