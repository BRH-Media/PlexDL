using System;
using System.Windows.Forms;
using PlexDL.Common;

namespace PlexDL
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler.Handler);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PlexDL.UI.Home());
        }
    }
}