using LogDel;
using LogDel.Enums;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using UIHelpers;

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
            var arr = args.ToList();

            //setup arguments and their associated actions
            VisualStyles(arr);
            CheckDevStatus(arr);
            CheckDebug(arr);
            CheckOverrideLogProtection(args);

            //set default values
            //ObjectProvider.PlexProviderDlAppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl";

            //check if the %APPDATA%\.plexdl folder is present. If it isn't then create it.
            CheckAppDataFolder();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;
            Application.SetCompatibleTextRenderingDefault(false);
            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;

            if (arr.Count > 0)
            {
                var firstArg = arr[0];
                //Windows will pass "Open With" files as the first argument; checking if the first argument
                //exists as a file will validate whether this has occurred.
                if (File.Exists(firstArg))
                {
                    //Windows has passed a file; we need to check if it's a '.pmxml' file which we can load
                    //into the Metadata window.
                    var ext = Path.GetExtension(firstArg);
                    const string expectedFormat = ".pxz";
                    if (string.Equals(ext, expectedFormat))
                    {
                        try
                        {
                            var metadata = ImportExport.MetadataFromFile(firstArg);
                            UiUtils.RunMetadataWindow(metadata, true);
                        }
                        catch (Exception ex)
                        {
                            LoggingHelpers.RecordException(ex.Message, @"StartupLoadPxz");
                            UIMessages.Error($"Error occurred whilst loading PXZ file:\n\n{ex}");
                        }
                    }
                    else
                    {
                        UIMessages.Error(@"PlexDL doesn't recognise this file-type: '" + ext + @"'",
                            @"Validation Error");
                    }
                }
                else
                {
                    if (arr.Contains("-tw"))
                        UiUtils.RunTestingWindow(true);
                    else if (arr.Contains("-t"))
                        UiUtils.RunTranslator(true);
                    else
                        UiUtils.RunPlexDlHome(true);
                }
            }
            else
            {
                Application.Run(new Home());
            }
        }

        private static void CheckOverrideLogProtection(ICollection<string> args)
        {
            //toggle log file (.logdel) DPAPI protection
            if (args.Contains("-lpon"))
                Vars.Protected = LogSecurity.Protected;
            else if (args.Contains("-lpoff"))
                Vars.Protected = LogSecurity.Unprotected;
        }

        private static void VisualStyles(ICollection<string> args)
        {
            if (args.Contains("-v1"))
                Application.EnableVisualStyles();
            else if (!args.Contains("-v0"))
                if (!args.Contains("-t"))
                    Application.EnableVisualStyles();
        }

        private static void CheckDevStatus(ICollection<string> args)
        {
            if (args.Contains("-b"))
                BuildState.State = DevStatus.InBeta;
            else if (args.Contains("-p"))
                BuildState.State = DevStatus.ProductionReady;
            else if (args.Contains("-d"))
                BuildState.State = DevStatus.InDevelopment;
        }

        private static void CheckDebug(ICollection<string> args)
        {
            Flags.IsDebug = args.Contains("-debug");
        }

        private static void CheckAppDataFolder()
        {
            if (!Directory.Exists(Strings.PlexDlAppData))
                Directory.CreateDirectory(Strings.PlexDlAppData);
        }
    }
}