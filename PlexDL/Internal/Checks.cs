using LogDel;
using LogDel.Enums;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Internal
{
    public static class Checks
    {
        private static ICollection<string> Args { get; } = Environment.GetCommandLineArgs().ToList();

        public static void OpenWith(string file, bool appRun = true)
        {
            //Windows has passed a file; we need to check what type it is
            var ext = Path.GetExtension(file);

            //UIMessages.Info(file);

            //check if it's a supported file-type
            if (CheckAgainstSupportedFiles(file))
            {
                //try the metadata import and then show it if successful
                try
                {
                    var metadata = MetadataImportExport.MetadataFromFile(file);
                    if (metadata != null)
                        UiUtils.RunMetadataWindow(metadata, appRun);
                    else
                        UIMessages.Error(@"Metadata parse failed; null result.");
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

        public static bool CheckAgainstSupportedFiles(string fileName)
        {
            //clean all periods from the extension
            var ext = Path.GetExtension(fileName).Replace(@".", @"");

            //what file types are valid?
            var supportedTypes = new[] { @"pxz", @"pmxml", @"prof" };

            //finally, perform the check itself
            return supportedTypes.Contains(ext.ToLower());
        }

        public static void FullArgumentCheck()
        {
            OverrideLogProtection();
            VisualStyles();
            _DevStatus();
            Debug();
        }

        public static void OverrideLogProtection()
        {
            //toggle log file (.logdel) DPAPI protection (REALLLLLY slow; please don't ever enable)
            if (Args.Contains("-lpon"))
                Vars.Protected = LogSecurity.Protected;
            else if (Args.Contains("-lpoff"))
                Vars.Protected = LogSecurity.Unprotected;
        }

        public static void VisualStyles()
        {
            if (Args.Contains("-v1"))
                Application.EnableVisualStyles();
            else if (!Args.Contains("-v0"))
                if (!Args.Contains("-t"))
                    Application.EnableVisualStyles();
        }

        public static void _DevStatus()
        {
            if (Args.Contains("-b"))
                BuildState.State = DevStatus.InBeta;
            else if (Args.Contains("-p"))
                BuildState.State = DevStatus.ProductionReady;
            else if (Args.Contains("-d"))
                BuildState.State = DevStatus.InDevelopment;
        }

        public static void Debug()
        {
            Flags.IsDebug = Args.Contains("-debug");
        }

        public static void TryLoadDefaultSettings()
        {
            try
            {
                //check if default settings have been created
                if (DefaultSettingsManager.SettingsExist)
                {
                    //try and load it with no messages
                    var defaultProfile = DefaultSettingsManager.LoadDefaultSettings();

                    //if it isn't null, then assign it to the global settings
                    if (defaultProfile != null)
                        ObjectProvider.Settings = defaultProfile;
                }
                else
                {
                    //create the file with no messages
                    if (ObjectProvider.Settings != null)
                        ObjectProvider.Settings.CommitDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, @"LoadDefaultProfileError");
            }
        }

        public static void CheckAppDataFolder()
        {
            if (!Directory.Exists(Strings.PlexDlAppData))
                Directory.CreateDirectory(Strings.PlexDlAppData);
        }
    }
}