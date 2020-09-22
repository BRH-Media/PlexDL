using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.Internal
{
    public static class Program
    {
        public static List<string> Args { get; set; }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            //assign to global
            Args = args.ToList();

            //set default values
            //ObjectProvider.PlexProviderDlAppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl";

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
            if (Args.Count > 0)
            {
                //when 'open with...' is used in Windows, the first argument will be the file
                var firstArg = Args[0];

                //MessageBox.Show(firstArg);

                //Windows will pass "Open With" files as the first argument; checking if the first argument
                //exists as a file will validate whether this has occurred.
                if (File.Exists(firstArg))
                    //run the routine to load the PXZ
                    Checks.OpenWith(firstArg);
                else
                    //first argument isn't a file (no 'Open With'); check if it's a form open instruction
                    FormCheck(Args);
            }
            else
                //no arguments; run as normal.
                UiUtils.RunPlexDlHome(true);
        }

        private static void FormCheck(ICollection<string> args)
        {
            if (args.Contains("-tw"))
                UiUtils.RunTestingWindow(true);
            else if (args.Contains("-t"))
                UiUtils.RunTranslator(true);
            else
                UiUtils.RunPlexDlHome(true);
        }
    }
}