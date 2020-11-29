using PlexDL.Common.API.CastAPI.PlayQueue;

namespace PlexDL.Common.API.CastAPI
{
    public class QueueResult
    {
        public string QueueUri { get; set; } = @"";
        public string QueueId { get; set; } = @"";
        public MediaContainer QueueObject { get; set; } = null;
        public bool QueueSuccess { get; set; } = false;
    }
}