using PlexDL.Common.CastAPI.PlayQueue;

namespace PlexDL.Common.CastAPI
{
    public class QueueResult
    {
        public string QueueUri { get; set; } = @"";
        public string QueueId { get; set; } = @"";
        public MediaContainer QueueObject { get; set; } = null;
        public bool QueueSuccess { get; set; } = false;
    }
}