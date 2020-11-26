using PlexDL.PlexAPI.LoginHandler.Auth.Enums;
using PlexDL.PlexAPI.LoginHandler.Auth.JSON;
using PlexDL.PlexAPI.LoginHandler.Security;
using PlexDL.PlexAPI.LoginHandler.UI;
using PlexDL.WaitWindow;
using System.Windows.Forms;

namespace PlexDL.PlexAPI.LoginHandler.Auth
{
    public static class AuthRoutine
    {
        public static AuthResult GetAuthToken()
        {
            var stored = TokenManager.StoredToken();

            if (string.IsNullOrEmpty(stored))
            {
                //request a new Auth ticket
                var init = NewInit();

                //check if the new ticket is valid (not null)
                if (init == null)
                    return new AuthResult() { Result = AuthStatus.IncorrectResponse }; //null return value always means incorrect JSON response

                using (var frm = new LoginWindow())
                {
                    frm.PlexRequestPin = init;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.Success)
                        {
                            if (frm.Result != null)
                            {
                                var t = frm.Result.AuthToken;
                                TokenManager.SaveToken(t);
                                return new AuthResult { Token = t, Result = AuthStatus.Success };
                            }

                            return new AuthResult { Result = AuthStatus.Failed };
                        }

                        return new AuthResult { Result = AuthStatus.Failed };
                    }

                    return new AuthResult { Result = AuthStatus.Cancelled };
                }
            }

            return new AuthResult { Token = stored, Result = AuthStatus.Success }; ;
        }

        private static PlexAuth NewInit()
        {
            return (PlexAuth)WaitWindow.WaitWindow.Show(NewInit_Callback, @"Getting a new ticket");
        }

        private static void NewInit_Callback(object sender, WaitWindowEventArgs e)
        {
            e.Result = PlexAuthHandler.NewPlexAuthPin();
        }
    }
}