using PlexDL.Common.Shodan.Enums;

namespace PlexDL.Common.Shodan
{
    public class TokenFinderResult
    {
        public TokenFinderStatus Status { get; set; } = TokenFinderStatus.TOKEN_NOT_FOUND;
        public string Token { get; set; } = @"";
    }
}