using System;
using System.IO;
using System.Linq;

namespace LogDel
{
    public class LogWriter
    {
        private static string CleanseLogDel(string line)
        {
            var clean = line;
            char[] bannedChars =
            {
                '#', '!'
            };
            foreach (var c in line)
                if (bannedChars.Contains(c))
                {
                    var index = clean.IndexOf(c);
                    clean.Remove(index, 1);
                }

            return clean;
        }

        public static void LogDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                var logdelLine = "";
                var fqFile = @"Logs\" + fileName;

                //log parsing will fail if the headers don't match the entry
                //it's the same as having 4 cells to a row; but only 3 columns...
                if (headers.Length == logEntry.Length)
                {
                    foreach (var l in logEntry)
                        logdelLine += l + "!";

                    logdelLine = CleanseLogDel(logdelLine.TrimEnd('!'));

                    var headersString = "###";
                    foreach (var h in headers)
                        headersString += h + "!";

                    headersString = CleanseLogDel(headersString.TrimEnd('!'));

                    if (File.Exists(fqFile))
                    {
                        var existing = File.ReadAllText(fqFile);
                        if (string.IsNullOrEmpty(existing))
                        {
                            var contentToWrite = headersString + "\n" + logdelLine;
                            File.WriteAllText(fqFile, contentToWrite);
                        }
                        else
                        {
                            var contentToWrite = existing + "\n" + logdelLine;
                            File.WriteAllText(fqFile, contentToWrite);
                        }
                    }
                    else
                    {
                        var contentToWrite = headersString + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                }
            }
            catch (Exception ex)
            {
                //for debugging only!
                //MessageBox.Show(ex);
                //ignore the error
            }
        }
    }
}