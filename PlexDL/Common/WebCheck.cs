using PlexDL.WaitWindow;
using System;
using System.Net;

namespace PlexDL.Common
{
    public class WebCheck
    {
        public Exception LastException { get; set; }
        public string StatusCode { get; set; } = "Undetermined";
        public bool ConnectionSuccess { get; set; }

        public static WebCheck TestUrl(string url, bool waitWindow = true)
        {
            WebCheck obj;

            if (waitWindow)
                obj = (WebCheck)WaitWindow.WaitWindow.Show(TestUrl_WaitWindow, @"Checking connection", url);
            else
                obj = TestUrl_Worker(url);

            return obj;
        }

        private static void TestUrl_WaitWindow(object sender, WaitWindowEventArgs e)
        {
            var url = (string)e.Arguments[0];
            e.Result = TestUrl_Worker(url);
        }

        private static WebCheck TestUrl_Worker(string url)
        {
            var obj = new WebCheck();
            try
            {
                //Creating the HttpWebRequest
                var request = (HttpWebRequest)WebRequest.Create(url);
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