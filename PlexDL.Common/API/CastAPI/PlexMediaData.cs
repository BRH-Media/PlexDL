using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Structures.Plex;
using SharpCaster.Models.ChromecastRequests;

namespace PlexDL.Common.API.CastAPI
{
    public static class PlexMediaData
    {
        /// <summary>
        /// Manually specify a server to prefill content data information
        /// </summary>
        /// <param name="content"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public static MediaData DataFromContent(PlexObject content, MyPlex.Server server)
        {
            //ID of content to play via Plex app
            var contentId = content.ApiUri;

            //custom data filled from Plex server provider
            var customData = CustomData.FillFromApiServer(server);

            //stream constants
            const string contentType = @"VIDEO";
            const StreamType streamType = StreamType.BUFFERED;

            //SharpCaster MediaData object provider for streaming media
            return new MediaData(contentId, contentType, null, streamType, 0D, customData);
        }

        /// <summary>
        /// Utilise the currently connected server to prefill content data information
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MediaData DataFromContent(PlexObject content)
            => DataFromContent(content, ObjectProvider.Svr);
    }
}