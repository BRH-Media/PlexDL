using PlexDL.PlexAPI.LoginHandler.Auth;
using PlexDL.WaitWindow;
using System.Windows.Forms;

namespace PlexDL.PlexAPI.LoginHandler
{
    public static class AuthRoutine
    {
        public static string GetAuthToken()
        {
            var stored = TokenManager.StoredToken();

            if (string.IsNullOrEmpty(stored))
            {
                //request a new Auth ticket
                var init = NewInit();

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
                                return t;
                            }
                            else
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
            }
            else
                return stored;
        }

        private static PlexPins NewInit()
        {
            return (PlexPins)WaitWindow.WaitWindow.Show(NewInit_Callback, @"Talking to Plex.tv");
        }

        private static void NewInit_Callback(object sender, WaitWindowEventArgs e)
        {
            e.Result = PlexPins.NewPlexAuthPin();
        }
    }
}