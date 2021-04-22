using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Web;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace PlexDL.Common.BarcodeHandler.QRCode
{
    public class QRProvider
    {
        public string CodeUrl { get; }
        public string CodeBase64 { get; set; } = "";
        public Image CodeImage { get; set; } = null;

        public QRProvider(string url)
        {
            //set URL
            CodeUrl = url;
        }

        public bool Fetch()
        {
            try
            {
                //validate URL
                if (Uri.TryCreate(CodeUrl, UriKind.Absolute, out var u))
                {
                    //URI valid, encode it
                    var codeUri = HttpUtility.UrlEncode(CodeUrl);

                    //code BackColor
                    var codeBackColor = HttpUtility.UrlEncode("#ffffff");

                    //URI valid, attempt data fetch
                    var fetchUrl = $"https://qrcode.tec-it.com/API/QRCode?data={codeUri}&backcolor={codeBackColor}&method=base64";

                    //fetch it using AltoHttp
                    var codeImageBase64 = ResourceGrab.GrabString(fetchUrl);

                    //null validation
                    if (!string.IsNullOrWhiteSpace(codeImageBase64))
                    {
                        //convert to bytes
                        var codeImageBytes = Convert.FromBase64String(codeImageBase64);

                        //length validation
                        if (codeImageBytes.Length > 0)
                        {
                            //convert to image
                            var bitmap = new Bitmap(new MemoryStream(codeImageBytes));

                            //null validation
                            if ((bitmap.Width * bitmap.Height) > 0)
                            {
                                //assign image
                                CodeImage = bitmap;

                                //success
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log error
                LoggingHelpers.RecordException(ex.Message, @"FetchQRCodeError");
            }

            //default return
            return false;
        }
    }
}