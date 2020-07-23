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
                    var result = days >= interval;
                    var logMessage = $"XML record {(result ? @"has" : @"has not")} expired [{days}/{interval}]";
                    LoggingHelpers.RecordCacheEvent(logMessage, $"file://{filePath}");
                    return result;
                }

                LoggingHelpers.RecordException(@"Specified cache file doesn't exist", @"CacheExpiryChkError");
                //default is true; this signifies that it has expired, so PlexDL will try and get a new copy.
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CacheExpiryChkError");
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
                var days = (int)(now - fileCreation).TotalDays + 1; //adding one ensures if it was created on the same day it counts as one day passed

                LoggingHelpers.RecordCacheEvent($"Requested XML record is {days} day(s) old", $"file://{filePath}");

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