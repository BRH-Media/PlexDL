using PlexDL.Common.Logging;
using System;
using System.Runtime.ExceptionServices;
using UIHelpers;

namespace PlexDL.Internal
{
    public static class UnhandledExceptionHandler
    {
        public static void CriticalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var obj = (Exception)e.ExceptionObject;
            CriticalExceptionHandler(obj);
        }

        public static void CriticalExceptionHandler(object sender, FirstChanceExceptionEventArgs e)
        {
            var obj = e.Exception;
            CriticalExceptionHandler(obj, true);
        }

        public static void CriticalExceptionHandler(Exception e, bool silent = false)
        {
            if (!silent)
                UIMessages.Error(
                    "An unhandled exception has occurred. Please report this issue on GitHub, including any relevant logs or other information.\n\n" +
                    e, @"Unhandled Exception");
            LoggingHelpers.RecordException(e.Message, "UnhandledException");
        }
    }
}