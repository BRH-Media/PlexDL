using PlexDL.Common.Logging;
using System;
using UIHelpers;

namespace PlexDL.Internal
{
    public static class UnhandledExceptionHandler
    {
        public static void CriticalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var obj = (Exception)e.ExceptionObject;
            UIMessages.Error(
                "An unhandled exception has occurred. Please report this issue on GitHub, including any relevant logs or other information.\n\n" +
                obj, @"Unhandled Exception");
            LoggingHelpers.RecordException(obj.Message, "UnhandledException");
        }
    }
}