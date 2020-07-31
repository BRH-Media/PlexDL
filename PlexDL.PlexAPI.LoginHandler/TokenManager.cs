using PlexDL.Common.Enums;
using PlexDL.Common.Security;
using System;
using System.IO;
using UIHelpers;

namespace PlexDL.PlexAPI.LoginHandler
{
    public static class TokenManager
    {
        private const string FILE = @".token";

        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string Final = $@"{AppData}\.plexdl\{FILE}";

        public static bool TokenCachingEnabled { get; set; } = true;
        public static bool IsTokenStored => File.Exists(Final);

        public static void TokenClearProcedure()
        {
            if (IsTokenStored)
            {
                const string q = @"Are you sure you want to clear your token?";

                //if the user clicks 'No' (false for 'No', true for 'Yes'), simply exit the function.
                if (!UIMessages.Question(q)) return;

                if (ClearStored())
                    UIMessages.Info(
                        @"Successfully cleared your Plex.tv token. It will be reinstated once you login via the Server Manager.");
                else
                    UIMessages.Error(
                        @"Couldn't clear your token, because an unknown error occured. Please delete it manually, and report this issue via GitHub.");
            }
            else
                UIMessages.Error(
                    @"Couldn't clear your token, because PlexDL has not saved it yet.");
        }

        public static string StoredToken()
        {
            try
            {
                if (!IsTokenStored || !TokenCachingEnabled) return string.Empty;

                var protectedToken = new ProtectedString(File.ReadAllText(Final), StringProtectionMode.Decrypt);

                var t = protectedToken.ProcessedValue; //decrypt via DPAPI
                return t.Length == 20 && !string.IsNullOrEmpty(t) ? t : string.Empty; //valid Plex tokens are always 20 characters in length.
            }
            catch (Exception)
            {
                //ignore any errors
                return string.Empty;
            }
        }

        public static bool SaveToken(string token, bool deleteIfPresent = true)
        {
            if (deleteIfPresent) ClearStored();

            try
            {
                var protectedToken = new ProtectedString(token, StringProtectionMode.Encrypt);
                if (TokenCachingEnabled)
                    File.WriteAllText(Final, protectedToken.ProcessedValue); //encrypt via DPAPI then write
                return true;
            }
            catch (Exception)
            {
                //ignore any errors
                return false;
            }
        }

        public static bool ClearStored()
        {
            if (!IsTokenStored) return false;

            try
            {
                File.Delete(Final);
                return true;
            }
            catch (Exception)
            {
                //ignore any errors
                return false;
            }
        }
    }
}