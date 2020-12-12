using PlexDL.Common.Logging;
using System;
using System.Runtime.ExceptionServices;
using UIHelpers;

namespace PlexDL.Internal
{
    /// <summary>
    /// Handles global critical exceptions that aren't handled elsewhere
    /// </summary>
    public static class UnhandledExceptionHandler
    {
        /// <summary>
        /// Internal critical unhandled exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CriticalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
            => CriticalExceptionHandler((Exception)e.ExceptionObject, true);

        /// <summary>
        /// Internal non-critical unhandled exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CriticalExceptionHandler(object sender, FirstChanceExceptionEventArgs e)
            => CriticalExceptionHandler(e.Exception, true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="silent"></param>
        public static void CriticalExceptionHandler(Exception e, bool silent = false)
        {
            //check if messages aren't disabled
            if (!silent)

                //alert the user to the error
                UIMessages.Error(
                    "An unhandled exception has occurred. Please report this issue on GitHub, including any relevant logs or other information.\n\n" +
                    e, @"Unhandled Exception");

            //log the exception
            LoggingHelpers.RecordException(e.Message, "UnhandledException");
        }
    }
}