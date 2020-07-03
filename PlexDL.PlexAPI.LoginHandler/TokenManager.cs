using System;
using System.IO;

namespace PlexDL.PlexAPI.LoginHandler
{
    public static class TokenManager
    {
        private static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string file = @".token";
        private static readonly string final = $@"{appdata}\.plexdl\{file}";

        public static bool TokenCachingEnabled { get; set; } = true;

        public static bool IsTokenStored => File.Exists(final);

        public static string StoredToken()
        {
            if (IsTokenStored && TokenCachingEnabled)
            {
                var t = File.ReadAllText(final);
                return ((t.Length == 20) && (!string.IsNullOrEmpty(t))) ? t : string.Empty; //valid Plex tokens are always 20 characters in length.
            }
            else
                return string.Empty;
        }

        public static bool SaveToken(string token, bool deleteIfPresent = true)
        {
            if (deleteIfPresent) ClearStored();

            try
            {
                if (TokenCachingEnabled)
                    File.WriteAllText(final, token);
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
            if (IsTokenStored)
            {
                try
                {
                    File.Delete(final);
                    return true;
                }
                catch (Exception)
                {
                    //ignore any errors
                    return false;
                }
            }
            else
                return false;
        }
    }
}