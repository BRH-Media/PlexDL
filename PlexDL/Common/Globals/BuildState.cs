using PlexDL.Common.Enums;

namespace PlexDL.Common.Globals
{
    public static class BuildState
    {
        public static DevStatus State { get; set; } = DevStatus.InDevelopment;
    }
}