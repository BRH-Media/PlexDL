using PlexDL.Common.API;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Security;
using PlexDL.Common.Structures.AppOptions;
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

        public static void CommitDefaultSettings(this ApplicationOptions settings)
        {
            try
            {
                //delete existing settings
                if (SettingsExist)
                    File.Delete(SettingsFile);

                //write all new settings
                File.WriteAllText(SettingsFile, ProtectedSettings
                    ? settings.EncryptSettings()
                    : settings.ProfileToXml());
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CommitToDefaultError");
            }
        }

        public static string EncryptSettings(this ApplicationOptions settings)
        {
            try
            {
                var settingsRaw = settings.ProfileToXml();
                var handler = new ProtectedString(settingsRaw, ProtectionMode.Encrypt);
                return $"{SettingsProtectionDelimiter}{handler.ProcessedValue}";
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"SettingsEncryptionError");
            }

            //default
            return @"";
        }

        public static string DecryptSettings()
        {
            try
            {
                var settingsRaw = SettingsContent.Remove(0, SettingsProtectionDelimiter.Length);
                var handler = new ProtectedString(settingsRaw, ProtectionMode.Decrypt);
                return handler.ProcessedValue;
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