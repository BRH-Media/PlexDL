using LogDel;
using LogDel.Enums;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Internal
{
    public static class ArgHandler
    {
        private static readonly ICollection<string> args = Environment.GetCommandLineArgs().ToList();

        public static void OpenWith(string file, bool appRun = true)
        {
            //Windows has passed a file; we need to check if it's a '.pxz' file which we can load
            //into the Metadata window.
            var ext = Path.GetExtension(file);
            const string expectedFormat = ".pxz";

            //check if it's a supported file-type
            if (string.Equals(ext, expectedFormat))
            {
                //try the metadata import and then show it if successful
                try
                {
                    var metadata = ImportExport.MetadataFromFile(file);
                    if (metadata != null) UiUtils.RunMetadataWindow(metadata, appRun);
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
            if (args.Contains("-lpon"))
                Vars.Protected = LogSecurity.Protected;
            else if (args.Contains("-lpoff"))
                Vars.Protected = LogSecurity.Unprotected;
        }

        public static void VisualStyles()
        {
            if (args.Contains("-v1"))
                Application.EnableVisualStyles();
            else if (!args.Contains("-v0"))
                if (!args.Contains("-t"))
                    Application.EnableVisualStyles();
        }

        public static void _DevStatus()
        {
            if (args.Contains("-b"))
                BuildState.State = DevStatus.InBeta;
            else if (args.Contains("-p"))
                BuildState.State = DevStatus.ProductionReady;
            else if (args.Contains("-d"))
                BuildState.State = DevStatus.InDevelopment;
        }

        public static void Debug()
        {
            Flags.IsDebug = args.Contains("-debug");
        }
    }
}