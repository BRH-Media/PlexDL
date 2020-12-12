using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.Internal
{
    /// <summary>
    /// Internal program code
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //global exception handlers
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;
            AppDomain.CurrentDomain.FirstChanceException += UnhandledExceptionHandler.CriticalExceptionHandler;
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve.HandleResolve;

            //no legacy
            Application.SetCompatibleTextRenderingDefault(false);

            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;

            //attempt to load default settings from AppData
            Checks.TryLoadDefaultSettings();

            //check if the %APPDATA%\.plexdl folder is present. If it isn't then create it.
            Checks.CheckAppDataFolder();

            //setup arguments and their associated actions
            Checks.FullArgumentCheck();

            //have any arguments been passed?
            if (Checks.Args.Count > 0)
            {
                //when 'open with...' is used in Windows, the first argument will be the file
                var firstArg = Checks.Args[0];

                //MessageBox.Show(firstArg);

                //Windows will pass "Open With" files as the first argument; checking if the first argument
                //exists as a file will validate whether this has occurred.
                if (File.Exists(firstArg))
                    //run the routine to load the PXZ
                    Checks.OpenWith(firstArg);
                else
                    //first argument isn't a file (no 'Open With'); check if it's a form open instruction
                    FormCheck(Checks.Args);
            }
            else
                //no arguments; run as normal.
                UIUtils.RunPlexDlHome(true);
        }

        private static void FormCheck(ICollection<string> args)
        {
            if (args.Contains("-tw"))
                UIUtils.RunTestingWindow(true);
            else if (args.Contains("-t"))
                UIUtils.RunTranslator(true);
            else
                UIUtils.RunPlexDlHome(true);
        }
    }
}