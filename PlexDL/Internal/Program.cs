using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Extensions;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
            //attempt to load default settings from AppData
            TryLoadDefaultSettings();

            //check if the %APPDATA%\.plexdl folder is present. If it isn't then create it.
            CheckAppDataFolder();

            //setup arguments and their associated actions
            ArgHandler.FullArgumentCheck();

            //set default values
            //ObjectProvider.PlexProviderDlAppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl";

            //global exception handlers
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;
            AppDomain.CurrentDomain.FirstChanceException += UnhandledExceptionHandler.CriticalExceptionHandler;


            //no legacy
            Application.SetCompatibleTextRenderingDefault(false);

            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;

            //have any arguments been passed?
            if (args.Length > 0)
            {
                //when 'open with...' is used in Windows, the first argument will be the file
                var firstArg = args[0];

                //Windows will pass "Open With" files as the first argument; checking if the first argument
                //exists as a file will validate whether this has occurred.
                if (File.Exists(firstArg))
                    //run the routine to load the PXZ
                    ArgHandler.OpenWith(firstArg);
                else
                    //first argument isn't a file (no 'Open With'); check if it's a form open instruction
                    FormCheck(args);
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

        private static void TryLoadDefaultSettings()
        {
            try
            {
                //path of the default settings file
                var path = $@"{Strings.PlexDlAppData}\.default";

                //check if default settings have been created
                if (File.Exists(path))
                {
                    //try and load it with no messages
                    var defaultProfile = ProfileImportExport.ProfileFromFile(path, true);

                    //if it isn't null, then assign it to the global settings
                    if (defaultProfile != null)
                        ObjectProvider.Settings = defaultProfile;
                }
                else
                {
                    //create the file with no messages
                    if (ObjectProvider.Settings != null)
                        ObjectProvider.Settings.SaveToDefault();
                }
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, @"LoadDefaultProfileError");
            }
        }

        private static void CheckAppDataFolder()
        {
            if (!Directory.Exists(Strings.PlexDlAppData))
                Directory.CreateDirectory(Strings.PlexDlAppData);
        }
    }
}