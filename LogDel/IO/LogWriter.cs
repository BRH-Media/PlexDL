using LogDel.Utilities;
using LogDel.Utilities.Extensions;
using System;
using System.IO;
using System.Linq;

namespace LogDel.IO
{
    public static class LogWriter
    {
        /// <summary>
        /// The directory where log files are read from/written to
        /// </summary>
        public static string LogDirectory { get; set; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.plexdl\logs";

        /// <summary>
        /// Allows for writing contents to a LogDel-formatted file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="headers"></param>
        /// <param name="logEntry"></param>
        public static void LogDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                //if the logging directory doesn't exist, create it
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                //join the logging directory and the requested log file name to form the path to write to
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
                var contentToWrite = !File.Exists(fqFile) || FileHelper.FileEmpty(fqFile) ? headersString + "\n" + logdelLine : logdelLine;

                //finalise the log file
                SecurityUtils.WriteLog(fqFile, contentToWrite, true);
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