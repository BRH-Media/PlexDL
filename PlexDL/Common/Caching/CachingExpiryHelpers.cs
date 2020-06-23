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
                    var days = GetDaysOld(filePath);
                    //DEBUG ONLY
                    //UIMessages.Info(days.ToString());
                    return days >= interval;
                }

                LoggingHelpers.RecordException("Specified cache file doesn't exist", "CacheExpiryChkError");
                //default is true; this signifies that it has expired, so PlexDL will try and get a new copy.
                return true;
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
                if (!File.Exists(filePath)) return 0;

                var fileCreation = File.GetCreationTime(filePath);
                var now = DateTime.Now;
                var days = (int)(now - fileCreation).TotalDays;

                return days;
                //default value
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "CacheAgeChkError");
                return 0;
            }
        }
    }
}