using LogDel;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using System;
using System.Diagnostics;
using System.Globalization;
using LogDel.IO;

namespace PlexDL.Common.Logging
{
    public static class LoggingHelpers
    {
        private static int _logIncrementer;

        public static void RecordGeneralEntry(string logEntry)
        {
            try
            {
                if (!ObjectProvider.Settings.Logging.EnableGenericLogDel) return;

                var finalEntry = $"[General] {logEntry}";

                _logIncrementer++;
                string[] headers =
                {
                    "EntryID", "SessionID", "DateTime", "Event"
                };
                string[] logEntryToAdd =
                {
                    _logIncrementer.ToString(), Strings.CurrentSessionId,
                    DateTime.Now.ToString(CultureInfo.CurrentCulture), finalEntry
                };

                LogWriter.LogDelWriter("PlexDL.logdel", headers, logEntryToAdd);
            }
            catch
            {
                //for debugging only!
                //UIMessages.Error(ex);
                //ignore the error
            }
        }

        public static void RecordCacheEvent(string eventEntry, string reqUrl = "Unknown")
        {
            try
            {
                if (!ObjectProvider.Settings.Logging.EnableCacheLogDel) return;

                var finalEntry = $"[Caching] {eventEntry}";

                string[] headers =
                {
                    "SessionID", "RequestedURL", "DateTime", "Entry"
                };
                string[] logEntryToAdd =
                {
                    Strings.CurrentSessionId, reqUrl, DateTime.Now.ToString(CultureInfo.CurrentCulture), finalEntry
                };

                LogWriter.LogDelWriter("Caching.logdel", headers, logEntryToAdd);
            }
            catch
            {
                //for debugging only!
                //UIMessages.Error(ex);
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
                if (!ObjectProvider.Settings.Logging.EnableExceptionLogDel) return;

                var finalMessage = $"[Exception] {message}";

                var stackTrace = new StackTrace();
                var function = stackTrace.GetFrame(1).GetMethod().Name;
                string[] headers =
                {
                    "SessionID", "DateTime", "ExceptionMessage", "OccurredIn", "ExceptionType"
                };
                string[] logEntry =
                {
                    Strings.CurrentSessionId, DateTime.Now.ToString(CultureInfo.CurrentCulture), finalMessage, function,
                    type
                };
                LogWriter.LogDelWriter("ExceptionLog.logdel", headers, logEntry);
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
                if (!ObjectProvider.Settings.Logging.EnableXmlTransactionLogDel) return;

                string[] headers =
                {
                    "SessionID", "DateTime", "Uri", "StatusCode"
                };
                string[] logEntry =
                {
                    Strings.CurrentSessionId, DateTime.Now.ToString(CultureInfo.CurrentCulture), uri, statusCode
                };
                LogWriter.LogDelWriter("TransactionLog.logdel", headers, logEntry);
            }
            catch
            {
                //ignore the error
            }
        }
    }
}