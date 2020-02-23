using PlexDL.UI;
using System;
using System.IO;

namespace PlexDL.Common.Logging
{
    public class LoggingHelpers
    {
        static int logIncrementer = 0;
        public static void addToLog(string logEntry)
        {
            logIncrementer++;
            string[] headers = { "ID", "DateTime", "Entry" };
            string[] logEntryToAdd = { logIncrementer.ToString(), DateTime.Now.ToString(), logEntry };
            if (Home.settings.Logging.EnableGenericLogDel)
                logDelWriter("PlexDL.logdel", headers, logEntryToAdd);
        }
        public static void recordException(string message, string type)
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
                logDelWriter("ExceptionLog.logdel", headers, LogEntry);
            }
        }

        public static void recordTransaction(string uri, string statusCode)
        {
            if (Home.settings.Logging.EnableXMLTransactionLogDel)
            {
                string[] headers = { "DateTime", "Uri", "StatusCode" };
                string[] LogEntry = { DateTime.Now.ToString(), uri, statusCode };
                logDelWriter("TransactionLog.logdel", headers, LogEntry);
            }
        }

        public static void logDelWriter(string fileName, string[] headers, string[] logEntry)
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
                    if (existing != "")
                    {
                        string contentToWrite = existing + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                    else
                    {
                        string contentToWrite = headersString + "\n" + logdelLine;
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
            }
        }
    }
}