using LogDel.Utilities.Extensions;
using PlexDL.Common.Enums;
using PlexDL.Common.Security;
using System;
using System.IO;
using System.Linq;

namespace LogDel
{
    public static class LogWriter
    {
        public static string LogDirectory { get; set; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.plexdl\logs";

        public static void LogDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                var fqFile = $"{LogDirectory}\\{fileName}";

                //log parsing will fail if the headers don't match the entry
                //it's the same as having 4 cells to a row; but only 3 columns...
                if (headers.Length != logEntry.Length) return;

                //remove forbidden characters from log entry like '!' and '#'
                logEntry = logEntry.CleanLogDel();

                var logdelLine = logEntry.Aggregate("", (current, l) => current + (l + "!"));

                //remove trailing '!'
                logdelLine = logdelLine.TrimEnd('!');

                //remove forbidden characters from log entry like '!' and '#'
                headers = headers.CleanLogDel();

                var headersString = headers.Aggregate("###", (current, h) => current + (h.CleanLogDel() + @"!"));

                //remove trailing '!'
                headersString = headersString.TrimEnd('!');

                //collection of content to write to the log at the end
                string contentToWrite;

                //does the log file already exist? (Note: if the log's encrypted DON'T append)
                if (File.Exists(fqFile) && Vars.Protected == LogSecurity.Unprotected)
                {
                    //it does, so we need to read the existing log entries into memory
                    var existing = File.ReadAllText(fqFile);

                    //check if the existing content is empty (maybe the file is blank?)
                    if (string.IsNullOrEmpty(existing))
                    {
                        //it is, so just rewrite the entire thing (including headers) without concatenating the original content (var existing)
                        contentToWrite = headersString + "\n" + logdelLine;
                    }
                    else
                    {
                        //the file isn't empty, so we need to concatenate the original content with the new content, then rewrite
                        //the log file
                        contentToWrite = existing + "\n" + logdelLine;
                    }
                }
                else
                {
                    //file doesn't exist, so create it and then write headers and the new entry
                    contentToWrite = headersString + "\n" + logdelLine;
                }

                //check if the content needs to be protected with DPAPI
                if (Vars.Protected == LogSecurity.Protected)
                {
                    //encrypt the log
                    var provider = new ProtectedString(contentToWrite, StringProtectionMode.Encrypt);

                    //replace the plainText contents with the new encrypted contents
                    contentToWrite = provider.ProcessedValue;
                }

                //finalise the log file
                File.WriteAllText(fqFile, contentToWrite);
            }
            catch (Exception)
            {
                //for debugging only!
                //MessageBox.Show(ex);
                //ignore the error
            }
        }
    }
}