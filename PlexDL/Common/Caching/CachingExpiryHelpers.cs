using PlexDL.Common.Logging;
using System;
using System.IO;

namespace PlexDL.Common.Caching
{
    public static class CachingExpiryHelpers
    {
        public static bool CheckCacheExpiry(string filePath, int interval)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    int days = GetDaysOld(filePath);
                    //DEBUG ONLY
                    //MessageBox.Show(days.ToString());
                    if (days >= interval)
                    {
                        //cache is expired; this signifies that it has expired, so PlexDL will try and get a new copy.
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    LoggingHelpers.RecordException("Specified cache file doesn't exist", "CacheExpiryChkError");
                    //default is true; this signifies that it has expired, so PlexDL will try and get a new copy.
                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "CacheExpiryChkError");
                //default is true; this signifies that it has expired, so PlexDL will try and get a new copy.
                return true;
            }
        }

        public static int GetDaysOld(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    DateTime fileCreation = File.GetCreationTime(filePath);
                    DateTime now = DateTime.Now;
                    int days = (int)(now - fileCreation).TotalDays;
                    return days;
                }
                //default value
                return 0;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "CacheAgeChkError");
                return 0;
            }
        }
    }
}