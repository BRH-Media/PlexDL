using LogDel;
using LogDel.Enums;
using PlexDL.Common;
using PlexDL.Common.API.PlexAPI.IO;
using PlexDL.Common.Globals;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Internal
{
    /// <summary>
    /// PlexDL command-line argument checks
    /// </summary>
    public static class Checks
    {
        /// <summary>
        /// Command-line arguments passed into PlexDL
        /// </summary>
        public static List<string> Args
        {
            get
            {
                //get all command-line args
                var args = Environment.GetCommandLineArgs().ToList();

                //remove first index (executable name)
                if (args.Count > 0)
                    args.RemoveAt(0);

                //return command-line args
                return args;
            }
        }

        /// <summary>
        /// Attempts to run PlexDL in 'Open With' mode
        /// </summary>
        /// <param name="file"></param>
        /// <param name="appRun"></param>
        public static void OpenWith(string file, bool appRun = true)
        {
            //Windows has passed a file; we need to check what type it is
            var ext = Path.GetExtension(file);

            //check if it's a supported file-type
            if (CheckAgainstSupportedFiles(file))
            {
                //try the metadata import and then show it if successful
                try
                {
                    var metadata = MetadataIO.MetadataFromFile(file);
                    if (metadata != null)
                        UIUtils.RunMetadataWindow(metadata, appRun);
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

        /// <summary>
        /// Determines whether the file specified is supported by PlexDL
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CheckAgainstSupportedFiles(string fileName)
        {
            //clean all periods from the extension
            var ext = Path.GetExtension(fileName).Replace(@".", @"");

            //what file types are valid?
            var supportedTypes = new[] { @"pxz", @"pmxml", @"prof" };

            //finally, perform the check itself
            return supportedTypes.Contains(ext.ToLower());
        }

        /// <summary>
        /// Execute all command-line argument checks
        /// </summary>
        public static void FullArgumentCheck()
        {
            OverrideLogProtection();
            VisualStyles();
            DevStatus();
            Debug();
        }

        /// <summary>
        /// Enables/disables WDPAPI in *.logdel files based on a command-line argument
        /// </summary>
        public static void OverrideLogProtection()
        {
            //toggle log file (.logdel) DPAPI protection (REALLLLLY slow; please don't ever enable)
            if (Args.Contains("-lpon"))
                Globals.Protected = LogSecurity.Protected;
            else if (Args.Contains("-lpoff"))
                Globals.Protected = LogSecurity.Unprotected;
        }

        /// <summary>
        /// Enables/disables visual styles based on a command-line argument
        /// </summary>
        public static void VisualStyles()
        {
            if (Args.Contains("-v1"))
                Application.EnableVisualStyles();
            else if (!Args.Contains("-v0"))
                if (!Args.Contains("-t"))
                    Application.EnableVisualStyles();
        }

        /// <summary>
        /// Sets the UI Development Status indicator based on a command-line argument
        /// </summary>
        public static void DevStatus()
        {
            if (Args.Contains("-b"))
                BuildState.State = Common.Enums.DevStatus.InBeta;
            else if (Args.Contains("-p"))
                BuildState.State = Common.Enums.DevStatus.ProductionReady;
            else if (Args.Contains("-d"))
                BuildState.State = Common.Enums.DevStatus.InDevelopment;
        }

        /// <summary>
        /// Sets the IsDebug flag based on a command-line argument
        /// </summary>
        public static void Debug()
        {
            Flags.IsDebug = Args.Contains("-debug");
        }

        /// <summary>
        /// Attempts to load PlexDL's '.default' file and apply the settings contained within
        /// </summary>
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
                    //create the file with no messages
                    new ApplicationOptions().CommitDefaultSettings();
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, @"LoadDefaultProfileError");
            }
        }

        /// <summary>
        /// If the PlexDL AppData folder doesn't exist, this will create it.
        /// </summary>
        public static void CheckAppDataFolder()
        {
            if (!Directory.Exists(Strings.PlexDlAppData))
                Directory.CreateDirectory(Strings.PlexDlAppData);
        }
    }
}