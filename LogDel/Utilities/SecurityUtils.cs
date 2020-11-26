using LogDel.Enums;
using LogDel.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace LogDel.Utilities
{
    public static class SecurityUtils
    {
        public static bool ProtectionEnabled()
        {
            return Globals.Protected == LogSecurity.Protected;
        }

        public static void MarkFile(string filePath, Mark fileMark)
        {
            try
            {
                //don't bother trying to mark the file if it doesn't exist
                if (!File.Exists(filePath)) return;

                switch (fileMark)
                {
                    case Mark.Encrypted:
                        //mark as Encrypted
                        File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Encrypted);
                        break;

                    case Mark.Decrypted:
                        //mark as Decrypted
                        File.SetAttributes(filePath, File.GetAttributes(filePath) & FileAttributes.Encrypted);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(fileMark), fileMark, null);
                }
            }
            catch (Exception)
            {
                //catch and ignore all exceptions
            }
        }

        //https://stackoverflow.com/questions/45880713/check-if-file-is-encrypted-or-not
        public static bool IsFileEncrypted(string filePath)
        {
            var attributes = File.GetAttributes(filePath);
            return (attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted;
        }

        public static List<string> DecryptLog(string cipherText, bool cleanLog = false)
        {
            try
            {
                //decrypt the log
                var decrypted = cipherText.DecryptString();

                //split up and return the new log
                return FileHelper.SplitLogLines(decrypted);
            }
            catch (Exception)
            {
                //catch and ignore all exceptions
                return new List<string>();
            }
        }

        public static void WriteLog(string filePath, string logContents, bool append = false)
        {
            try
            {
                var existingContents = @"";
                if (append && File.Exists(filePath)) existingContents = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(existingContents) && ProtectionEnabled())
                {
                    //decrypt and save the log to memory
                    existingContents = existingContents.DecryptString();
                }

                //content to save to the final file (ASCII)
                var contentToWrite = !string.IsNullOrEmpty(existingContents) ? $"{existingContents}\n{logContents}" : logContents;

                var finalContent = ProtectionEnabled()
                    ? contentToWrite.EncryptString()
                    : contentToWrite;

                //finalise the log file
                File.WriteAllText(filePath, finalContent);

                //decide marking
                MarkFile(filePath, ProtectionEnabled() ? Mark.Encrypted : Mark.Decrypted);
            }
            catch (Exception)
            {
                //catch and ignore all exceptions
            }
        }
    }
}