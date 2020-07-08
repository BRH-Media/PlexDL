using PlexDL.PlexAPI.LoginHandler.Auth.Enums;

namespace PlexDL.PlexAPI.LoginHandler.Auth
{
    public class AuthObject
    {
        public AuthResult Result { get; set; } = AuthResult.Error;
        public string Token { get; set; }
    }
}