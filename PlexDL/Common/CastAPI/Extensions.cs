using PlexDL.Common.Structures.Plex;
using SharpCaster;
using SharpCaster.Channels;
using SharpCaster.Models;
using SharpCaster.Models.ChromecastRequests;

namespace PlexDL.Common.CastAPI
{
    public static class Extensions
    {
        public static async void LoadPlexMedia(this PlexChannel channel, ChromeCastClient client, PlexObject content)
        {
            var mediaObject = PlexMediaData.DataFromContent(content);
            var req = new LoadRequest(client.CurrentApplicationSessionId, mediaObject, true, 0, mediaObject.CustomData);

            var reqJson = req.ToJson();
            await channel.Write(MessageFactory.Load(client.CurrentApplicationTransportId, reqJson));
        }
    }
}