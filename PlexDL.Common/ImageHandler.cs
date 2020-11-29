using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.Image;
using PlexDL.Common.Structures.Plex;
using PlexDL.ResourceProvider.Properties;
using PlexDL.WaitWindow;
using System;
using System.Drawing;
using System.Net;

namespace PlexDL.Common
{
    public static class ImageHandler
    {
        public static Bitmap GetImageFromUrl(string url, bool forceNoCache = false)
        {
            try
            {
                CachingHelpers.CacheStructureBuilder();
                if (string.IsNullOrEmpty(url))
                    return Resources.image_not_available_png_8;

                if (!forceNoCache)
                    if (ThumbCaching.ThumbInCache(url))
                        return ThumbCaching.ThumbFromCache(url);
            }
            catch (UnauthorizedAccessException ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ThumbIOAccessError");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ImageFetchError");
                return Resources.image_not_available_png_8;
            }

            return ForceImageFromUrl(url);
        }

        public static Bitmap ForceImageFromUrl(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var result = stream != null
                        ? (Bitmap)Image.FromStream(stream)
                        : Resources.image_not_available_png_8;
                    ThumbCaching.ThumbToCache(result, url);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ImageFetchError");
                return Resources.image_not_available_png_8;
            }
        }

        public static Bitmap GetPoster(PlexObject stream, bool waitWindow = true)
        {
            if (waitWindow)
                return (Bitmap)WaitWindow.WaitWindow.Show(GetPoster, @"Grabbing poster", stream);

            var result = GetImageFromUrl(stream.StreamInformation.ContentThumbnailUri);

            if (result == Resources.image_not_available_png_8) return result;
            if (!ObjectProvider.Settings.Generic.AdultContentProtection) return result;

            return Methods.AdultKeywordCheck(stream) ? Pixelation.Pixelate(result, 64) : result;
        }

        private static void GetPoster(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count == 1)
            {
                var c = (PlexObject)e.Arguments[0];
                e.Result = GetPoster(c, false);
            }
        }
    }
}