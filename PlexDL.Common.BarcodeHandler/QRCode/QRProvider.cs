using inet;
using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Web;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace PlexDL.Common.BarcodeHandler.QRCode
{
    public class QRProvider
    {
        public string CodeUrl { get; }
        public string CodeBase64 { get; set; } = "";
        public Image CodeImage { get; set; }

        public QRProvider(string url)
        {
            //set URL
            CodeUrl = url;
        }

        public bool Fetch()
        {
            try
            {
                //is there a valid internet connection?
                if (Internet.IsConnected)
                {
                    //validate URL
                    if (Uri.TryCreate(CodeUrl, UriKind.Absolute, out _))
                    {
                        //URI valid, encode it
                        var codeUri = HttpUtility.UrlEncode(CodeUrl);

                        //code BackColor
                        var codeBackColor = HttpUtility.UrlEncode("#ffffff");

                        //URI valid, attempt data fetch
                        var fetchUrl = $"https://qrcode.tec-it.com/API/QRCode?data={codeUri}&backcolor={codeBackColor}";

                        //fetch it using AltoHttp
                        var codeImageBytes = ResourceGrab.GrabBytes(fetchUrl);

                        //length validation
                        if (codeImageBytes?.Length > 0)
                        {
                            //convert to image
                            var bitmap = new Bitmap(new MemoryStream(codeImageBytes));

                            //null validation
                            if (bitmap.Width * bitmap.Height > 0)
                            {
                                //assign image
                                CodeImage = bitmap;

                                //success
                                return true;
                            }
                            else
                            {
                                //log error
                                LoggingHelpers.RecordException("QR code generation failed because the returned image was invalid", @"FetchQRCodeError");
                            }
                        }
                        else
                        {
                            //log error
                            LoggingHelpers.RecordException("QR code generation failed because the returned image was null", @"FetchQRCodeError");
                        }
                    }
                    else
                    {
                        //log error
                        LoggingHelpers.RecordException("QR code generation failed because the provided URL was invalid", @"FetchQRCodeError");
                    }
                }
                else
                {
                    //log error
                    LoggingHelpers.RecordException("QR code generation failed because no internet connection was discovered", @"FetchQRCodeError");
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