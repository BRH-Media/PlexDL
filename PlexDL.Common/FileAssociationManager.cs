using Microsoft.Win32;
using PlexDL.Common.Structures;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PlexDL.Common
{
    /// <summary>
    /// Manage PlexDL Shell extensions and file associations
    /// </summary>
    public static class FileAssociationManager
    {
        // needed so that Explorer windows get refreshed after the registry is updated
        [DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;

        public static void EnsureAssociationsSet()
        {
            //PlexDL exe
            var filePath = Process.GetCurrentProcess().MainModule?.FileName;

            //PXZ
            var pxzMetadata = new FileAssociation
            {
                Extension = ".pxz",
                ProgId = "PlexDL_PXZ_File",
                FileTypeDescription = "PlexDL Packed File",
                ExecutableFilePath = filePath
            };

            //XML (metadata)
            var xmlMetadata = new FileAssociation
            {
                Extension = ".pmxml",
                ProgId = "PlexDL_XML_File",
                FileTypeDescription = "PlexDL XML Metadata File",
                ExecutableFilePath = filePath
            };

            //XML (profile)
            var xmlProfile = new FileAssociation
            {
                Extension = ".prof",
                ProgId = "PlexDL_XML_Profile",
                FileTypeDescription = "PlexDL XML Profile",
                ExecutableFilePath = filePath
            };

            //add associations
            EnsureAssociationsSet(pxzMetadata);
            EnsureAssociationsSet(xmlMetadata);
            EnsureAssociationsSet(xmlProfile);
        }

        public static void EnsureAssociationsSet(params FileAssociation[] associations)
        {
            var madeChanges = false;

            foreach (var association in associations)
            {
                madeChanges |= SetAssociation(
                    association.Extension,
                    association.ProgId,
                    association.FileTypeDescription,
                    association.ExecutableFilePath);
            }

            if (madeChanges)
            {
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static bool SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath)
        {
            var madeChanges = false;

            //get rid of explorer override
            var currentUser = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" + extension, true);
            currentUser?.DeleteSubKey("UserChoice", false);
            currentUser?.Close();

            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + extension, progId);
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + progId, fileTypeDescription);
            madeChanges |= SetKeyDefaultValue($@"Software\Classes\{progId}\shell\open\command", "\"" + applicationFilePath + "\" \"%1\"");
            return madeChanges;
        }

        private static bool SetKeyDefaultValue(string keyPath, string value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(keyPath))
            {
                if (key?.GetValue(null) as string != value)
                {
                    key?.SetValue(null, value);
                    return true;
                }
            }

            return false;
        }
    }
}