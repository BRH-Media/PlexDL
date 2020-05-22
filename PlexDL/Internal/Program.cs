using PlexDL.Common.API;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Structures.Plex;
using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
            CheckDevStatus(arr);
            CheckDebug(arr);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler.CriticalExceptionHandler;
            Application.SetCompatibleTextRenderingDefault(false);
            //allow heaps of concurrent connections - this might make a difference for performance?
            //disable if you have any problems by commenting this line
            ServicePointManager.DefaultConnectionLimit = 128;

            if (arr.Count > 0)
            {
                string firstArg = arr[0];
                //Windows will pass "Open With" files as the first argument; checking if the first argument
                //exists as a file will validate whether this has occurred.
                if (File.Exists(firstArg))
                {
                    //Windows has passed a file; we need to check if it's a '.pmxml' file which we can load
                    //into the Metadata window.
                    string ext = Path.GetExtension(firstArg);
                    string expectedFormat = ".pmxml";
                    if (string.Equals(ext, expectedFormat))
                    {
                        var metadata = ImportExport.MetadataFromFile(firstArg);
                        RunMetadataWindow(metadata);
                    }
                    else
                        MessageBox.Show("PlexDL doesn't recognise this file-type: '" + ext + "'", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (arr.Contains("-testing"))
                        Application.Run(new TestForm());
                    else if (arr.Contains("-t"))
                        Application.Run(new Translator());
                    else
                        Application.Run(new Home());
                }
            }
            else
                Application.Run(new Home());
        }

        private static void RunMetadataWindow(PlexObject metadata)
        {
            var form = new Metadata();
            if (metadata != null)
            {
                form.StreamingContent = metadata;
                Application.Run(form);
            }
            else
                MessageBox.Show("Invalid PlexMovie Metadata File; the decoded data was null.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static void CheckDevStatus(List<string> args)
        {
            if (args.Contains("-beta"))
                BuildState.State = DevStatus.IN_BETA;
            else if (args.Contains("-prod"))
                BuildState.State = DevStatus.PRODUCTION_READY;
            else if (args.Contains("-dev"))
                BuildState.State = DevStatus.IN_DEVLOPMENT;
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