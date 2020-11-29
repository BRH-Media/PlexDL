using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Structures.Plex;
using SharpCaster.Models.ChromecastRequests;

namespace PlexDL.Common.API.CastAPI
{
    public static class PlexMediaData
    {
        public static MediaData DataFromContent(PlexObject content, MyPlex.Server server)
        {
            var contentId = content.ApiUri;
            var customData = CustomData.FillFromApiServer(server);
            var contentType = @"video";
            const StreamType streamType = StreamType.BUFFERED;

            return new MediaData(contentId, contentType, null, streamType, 0D, customData);
        }

        public static MediaData DataFromContent(PlexObject content)
        {
            return DataFromContent(content, ObjectProvider.Svr);
        }
    }
}