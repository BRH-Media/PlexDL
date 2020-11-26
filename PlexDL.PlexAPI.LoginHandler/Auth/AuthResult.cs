namespace PlexDL.PlexAPI.LoginHandler.Auth
{
    public class AuthResult
    {
        public Enums.AuthStatus Result { get; set; } = Enums.AuthStatus.Error;
        public string Token { get; set; }
    }
}