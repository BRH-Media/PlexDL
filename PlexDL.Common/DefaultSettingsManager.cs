using PlexDL.Common.API.PlexAPI.IO;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Security.Protection;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.WaitWindow;
using System;
using System.IO;

namespace PlexDL.Common
{
    public static class DefaultSettingsManager
    {
        /// <summary>
        ///     When enabled, WDPAPI encryption will be applied to the settings file
        /// </summary>
        public static bool ProtectedSettings { get; set; } = true;

        public static string SettingsFile { get; } = $@"{Strings.PlexDlAppData}\.default";
        public static bool SettingsExist => File.Exists(SettingsFile);
        public static string SettingsContent => SettingsExist ? File.ReadAllText(SettingsFile) : @"";
        private static string SettingsProtectionDelimiter { get; } = @"protected__";
        public static bool SettingsEncrypted => SettingsContent.StartsWith(SettingsProtectionDelimiter);
        public static string SettingsXml => SettingsExist ? (SettingsEncrypted ? DecryptSettings() : SettingsContent) : @"";

        public static ApplicationOptions LoadDefaultSettings()
        {
            try
            {
                if (SettingsExist)
                {
                    var settings = SettingsXml.ProfileFromXml();
                    if (settings != null)
                        return settings;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CommitToDefaultError");
            }

            //default
            return new ApplicationOptions();
        }

        private static void CommitDefaultSettings(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count == 1)
            {
                var settings = (ApplicationOptions)e.Arguments[0];
                e.Result = settings.CommitDefaultSettings(false);
            }
        }

        public static bool CommitDefaultSettings(this ApplicationOptions settings, bool waitWindow = true)
        {
            try
            {
                if (waitWindow)
                    return (bool)WaitWindow.WaitWindow.Show(CommitDefaultSettings, @"Saving settings", settings);
                else
                {
                    //write all new settings
                    var protectedFile = new ProtectedFile(SettingsFile);
                    protectedFile.WriteAllText(settings.ProfileToXml(), ProtectedSettings);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CommitToDefaultError");
            }

            //default
            return false;
        }

        public static string DecryptSettings()
        {
            try
            {
                var protectedFile = new ProtectedFile(SettingsFile);
                return protectedFile.ReadAllText();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"SettingsDecryptionError");
            }

            //default
            return @"";
        }
    }
}