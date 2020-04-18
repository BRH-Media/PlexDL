using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.Common
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;

            Application.SetCompatibleTextRenderingDefault(false);
            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;

            List<string> arr = args.ToList();

            if (arr.Contains("-t"))
                Application.Run(new Translator());
            else
                Application.Run(new Home());
        }
    }
}