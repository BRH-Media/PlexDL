using PlexDL.Common.BarcodeHandler.QRCodeHandler.OnlineProvider;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using QRCoder;
using System;
using System.Drawing;

namespace PlexDL.Common.BarcodeHandler.QRCodeHandler
{
    public static class QRCodeManager
    {
        public static Bitmap GetCodeImage(string url)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(url))
                {
                    //what should we do?
                    switch (ObjectProvider.Settings.Generic.GlobalQrFetchMode)
                    {
                        //online mode
                        case Enums.QRCodeFetchMode.Online:

                            //code generation handler
                            var codeImage = new QROnlineProvider(url);

                            //generate code
                            if (codeImage.Fetch())
                            {
                                //apply new image if not null
                                if (codeImage.CodeImage != null)
                                {
                                    //return resulting barcode
                                    return (Bitmap)codeImage.CodeImage;
                                }
                            }

                            //exit
                            break;

                        //offline mode
                        case Enums.QRCodeFetchMode.Offline:

                            //new QRCoder instance
                            var qrCoder = new QRCodeGenerator();

                            //generate the code
                            var codeData = qrCoder.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);

                            //create a new code based on the data
                            var code = new QRCode(codeData);

                            //grab the image component
                            var image = code.GetGraphic(20);

                            //validation
                            if (image != null)
                            {
                                //return the completed QR Code
                                return image;
                            }

                            //exit
                            break;
                    }
                }
                else
                {
                    //log the error
                    LoggingHelpers.RecordException(@"Couldn't generate QR code because the URL was invalid", @"QRCodeManagerException");
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"QRCodeManagerException");
            }

            //default
            return null;
        }
    }
}