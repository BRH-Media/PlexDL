using PlexDL.Common.Security.Protection;
using PlexDL.Common.Shodan.Globals;
using System.IO;

// ReSharper disable InvertIf

namespace PlexDL.Common.Shodan
{
    public static class ApiKeyManager
    {
        /// <summary>
        /// Stores a new Shodan API key.
        /// </summary>
        /// <returns></returns>
        public static bool StoreNewApiKey(string apiKey)
        {
            try
            {
                //validation;
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    //validation; Shodan API keys are 32 characters in length
                    if (apiKey.Length == 32)
                    {
                        //store the new GUID encrypted via WDPAPI
                        var handler = new ProtectedFile(Strings.ShodanApiKeyFileLocation);
                        handler.WriteAllText(apiKey);

                        //success
                        return true;
                    }
                }
            }
            catch
            {
                //ignore all errors
            }

            //default
            return false;
        }

        /// <summary>
        /// Load the Shodan API key from persistent storage; returns an empty string on failure.
        /// </summary>
        /// <returns></returns>
        public static string StoredShodanApiKey()
        {
            try
            {
                if (File.Exists(Strings.ShodanApiKeyFileLocation))
                {
                    //decrypt and read the API key from persistent storage
                    var handler = new ProtectedFile(Strings.ShodanApiKeyFileLocation);
                    var apiKey = handler.ReadAllText();

                    //is it a valid string?
                    if (!string.IsNullOrWhiteSpace(apiKey))
                    {
                        //is it a valid API key?
                        if (apiKey.Length == 32)
                        {
                            //return the stored API key
                            return apiKey;
                        }
                    }
                }
            }
            catch
            {
                //ignore all errors
            }

            //default
            return @"";
        }
    }
}