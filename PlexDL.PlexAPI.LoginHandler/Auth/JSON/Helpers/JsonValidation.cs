using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace PlexDL.PlexAPI.LoginHandler.Auth.JSON.Helpers
{
    public static class JsonValidation
    {
        //CREDIT: https://stackoverflow.com/a/14977915
        public static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) &&
                (!strInput.StartsWith("[") || !strInput.EndsWith("]"))) return false;
            try
            {
                var _ = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                Console.WriteLine(jex.Message);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}