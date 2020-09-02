using PlexDL.Common.API;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.IO;

namespace PlexDL.Common.Extensions
{
    public static class SaveToDefaultExt
    {
        /// <summary>
        /// Export this ApplicationOptions object to the default settings file
        /// </summary>
        /// <param name="options"></param>
        public static void SaveToDefault(this ApplicationOptions options)
        {
            try
            {
                //if the PlexDL AppData hasn't been made yet, then make it.
                if (!Directory.Exists(Strings.PlexDlAppData))
                    Directory.CreateDirectory(Strings.PlexDlAppData);

                //if the file already exists, delete it (otherwise duplication of values could occur)
                if (File.Exists(Strings.PlexDlDefault))
                    File.Delete(Strings.PlexDlDefault);

                //export to default settings with no messages
                ProfileImportExport.ProfileToFile(Strings.PlexDlDefault, options, true);
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, @"DefaultExportError");
            }
        }
    }
}