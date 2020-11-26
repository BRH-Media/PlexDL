using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogDel.Utilities
{
    public static class FileHelper
    {
        public static List<string> SplitLogLines(string logContents)
        {
            try
            {
                //split at the newline character to format a string into its separate lines
                return logContents.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch (Exception)
            {
                //catch and ignore all errors
                return new List<string>();
            }
        }

        public static bool FileEmpty(string filePath)
        {
            try
            {
                return new FileInfo(filePath).Length == 0;
            }
            catch (Exception)
            {
                //catch and ignore all errors
                return true;
            }
        }
    }
}