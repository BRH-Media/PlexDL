using PlexDL.Common.Globals;
using System;
using System.Diagnostics;
using LogDel;

namespace PlexDL.Common.Logging
{
    public static class LoggingHelpers
    {
        private static int logIncrementer;

        public static void RecordGenericEntry(string logEntry)
        {
            try
            {
                logIncrementer++;
                string[] headers =
                {
                    "SessionID","EntryID", "DateTime", "Entry"
                };
                string[] logEntryToAdd =
                {
                    GlobalStaticVars.CurrentSessionId, logIncrementer.ToString(), DateTime.Now.ToString(), logEntry
                };
                if (GlobalStaticVars.Settings.Logging.EnableGenericLogDel)
                    LogWriter.LogDelWriter("PlexDL.logdel", headers, logEntryToAdd);
            }
            catch (Exception ex)
            {
                //for debugging only!
                //MessageBox.Show(ex);
                //ignore the error
            }
        }

        public static void RecordCacheEvent(string eventEntry, string reqUrl = "Unknown")
        {
            try
            {
                string[] headers =
                {
                    "SessionID", "RequestedURL", "DateTime", "Entry"
                };
                string[] logEntryToAdd =
                {
                    GlobalStaticVars.CurrentSessionId, reqUrl, DateTime.Now.ToString(), eventEntry
                };
                if (GlobalStaticVars.Settings.Logging.EnableGenericLogDel)
                    LogWriter.LogDelWriter("Caching.logdel", headers, logEntryToAdd);
            }
            catch (Exception ex)
            {
                //for debugging only!
                //MessageBox.Show(ex);
                //ignore the error
            }
        }

        public static void RecordException(string message, string type)
        {
            try
            {
                ////Options weren't too great performance-wise, so I ended up using a stack-walk.
                ////If there's minimal errors happening at once, this shouldn't be a problem, otherwise disable
                ////The in-app setting to prevent this method from firing.
                if (GlobalStaticVars.Settings.Logging.EnableExceptionLogDel)
                {
                    var stackTrace = new StackTrace();
                    var function = stackTrace.GetFrame(1).GetMethod().Name;
                    string[] headers =
                    {
                        "SessionID", "DateTime", "ExceptionMessage", "OccurredIn", "ExceptionType"
                    };
                    string[] LogEntry =
                    {
                        GlobalStaticVars.CurrentSessionId, DateTime.Now.ToString(), message, function, type
                    };
                    LogWriter.LogDelWriter("ExceptionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
            }
        }

        public static void RecordTransaction(string uri, string statusCode)
        {
            try
            {
                if (GlobalStaticVars.Settings.Logging.EnableXMLTransactionLogDel)
                {
                    string[] headers =
                    {
                        "SessionID","DateTime", "Uri", "StatusCode"
                    };
                    string[] LogEntry =
                    {
                        GlobalStaticVars.CurrentSessionId, DateTime.Now.ToString(), uri, statusCode
                    };
                    LogWriter.LogDelWriter("TransactionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
            }
        }
    }
}