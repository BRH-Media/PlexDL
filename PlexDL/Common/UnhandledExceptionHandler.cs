using System;
using System.Windows.Forms;
using PlexDL.Common.Logging;

namespace PlexDL.Common
{
    public static class UnhandledExceptionHandler
    {
        public static void CriticalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var obj = (Exception)e.ExceptionObject;
            MessageBox.Show(
                "An unhandled exception has occurred. Please report this issue on GitHub, including any relevant logs or other information.\n\n" +
                obj, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LoggingHelpers.RecordException(obj.Message, "UnhandledException");
        }
    }
}