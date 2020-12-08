using GitHubUpdater.Net.DownloadManager;
using Newtonsoft.Json;
using PlexDL.Common.Security.Hashing;
using System;
using System.Globalization;
using System.IO;

namespace GitHubUpdater
{
    public static class Extensions
    {
        /// <summary>
        /// Indents and formats a JSON string<br />
        /// CREDIT: https://stackoverflow.com/a/21407175
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string FormatJson(this string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        /// <summary>
        /// Saves an exception object to a file
        /// </summary>
        /// <param name="ex"></param>
        public static void ExportError(this Exception ex)
        {
            try
            {
                //Date and time that this is being written
                var logTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);

                //file name is MD5-hashed current date and time (for uniqueness)
                var fileName = $"UpdateError_{MD5Helper.CalculateMd5Hash(logTime)}.log";

                //store all errors in the UpdateDirectory
                var errorsPath = $@"{Agent.UpdateDirectory}\errors";

                //ensure the 'errors' folder exists
                if (!Directory.Exists(errorsPath))
                    Directory.CreateDirectory(errorsPath);

                //full path
                var filePath = $@"{errorsPath}\{fileName}";

                //file contents
                var fileWrite = $"Occurred: {logTime}\n---\n\n{ex}";

                //export error to log
                File.WriteAllText(filePath, fileWrite);
            }
            catch
            {
                //ignore
            }
        }
    }
}