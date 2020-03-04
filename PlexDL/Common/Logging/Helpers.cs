using PlexDL.UI;
using System;
using System.IO;

namespace PlexDL.Common.Logging
{
    public static class LoggingHelpers
    {
        private static int logIncrementer = 0;

        public static void AddToLog(string logEntry)
        {
            try
            {
                logIncrementer++;
                string[] headers = { "ID", "DateTime", "Entry" };
                string[] logEntryToAdd = { logIncrementer.ToString(), DateTime.Now.ToString(), logEntry };
                if (Home.settings.Logging.EnableGenericLogDel)
                    LogDelWriter("PlexDL.logdel", headers, logEntryToAdd);
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void RecordException(string message, string type)
        {
            try
            {
                ////Options weren't too great performance-wise, so I ended up using a stack-walk.
                ////If there's minimal errors happening at once, this shouldn't be a problem, otherwise disable
                ////The in-app setting to prevent this method from firing.
                if (Home.settings.Logging.EnableExceptionLogDel)
                {
                    var stackTrace = new System.Diagnostics.StackTrace();
                    string function = stackTrace.GetFrame(1).GetMethod().Name;
                    string[] headers = { "DateTime", "ExceptionMessage", "OccurredIn", "ExceptionType" };
                    string[] LogEntry = { DateTime.Now.ToString(), message, function, type };
                    LogDelWriter("ExceptionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void RecordTransaction(string uri, string statusCode)
        {
            try
            {
                if (Home.settings.Logging.EnableXMLTransactionLogDel)
                {
                    string[] headers = { "DateTime", "Uri", "StatusCode" };
                    string[] LogEntry = { DateTime.Now.ToString(), uri, statusCode };
                    LogDelWriter("TransactionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void LogDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                if (!System.IO.Directory.Exists("Logs"))
                    System.IO.Directory.CreateDirectory("Logs");

                string logdelLine = "";
                string fqFile = @"Logs\" + fileName;

                foreach (string l in logEntry)
                {
                    logdelLine += l + "!";
                }
                logdelLine = logdelLine.TrimEnd('!');

                string headersString = "###";
                foreach (string h in headers)
                {
                    headersString += h + "!";
                }
                headersString = headersString.TrimEnd('!');

                if (File.Exists(fqFile))
                {
                    string existing = File.ReadAllText(fqFile);
                    if (string.IsNullOrEmpty(existing))
                    {
                        string contentToWrite = headersString + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                    else
                    {
                        string contentToWrite = existing + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                }
                else
                {
                    string contentToWrite = headersString + "\n" + logdelLine;
                    File.WriteAllText(fqFile, contentToWrite);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }
    }
}