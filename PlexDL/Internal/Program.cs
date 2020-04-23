using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.Internal
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            List<string> arr = args.ToList();
            VisualStyles(arr);
            CheckBeta(arr);
            CheckDebug(arr);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;
            Application.SetCompatibleTextRenderingDefault(false);
            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;
            if (arr.Contains("-t"))
                Application.Run(new Translator());
            else
                Application.Run(new Home());
        }

        private static void VisualStyles(List<string> args)
        {
            if (args.Contains("-v1"))
                Application.EnableVisualStyles();
            else
                if (!args.Contains("-v0"))
                if (!args.Contains("-t"))
                    Application.EnableVisualStyles();
        }

        private static void CheckBeta(List<string> args)
        {
            if (args.Contains("-b"))
                Common.Flags.IsBeta = true;
            else
                if (args.Contains("-nb"))
                Common.Flags.IsBeta = false;
        }

        private static void CheckDebug(List<string> args)
        {
            if (args.Contains("-debug"))
                Common.Flags.IsDebug = true;
            else
                Common.Flags.IsDebug = false;
        }
    }
}