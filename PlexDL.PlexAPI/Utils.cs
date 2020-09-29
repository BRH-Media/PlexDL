using System;

namespace PlexDL.MyPlex
{
    public static class Utils
    {
        public static DateTime GetDateTimeFromTimestamp(string timestamp)
        {
            try
            {
                if (DateTime.TryParse(timestamp, out var result))
                    return result;

                // First make a System.DateTime equivalent to the UNIX Epoch.
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                // Add the number of seconds in UNIX timestamp to be converted.
                return dateTime.AddSeconds(double.Parse(timestamp));
            }
            catch
            {
                // If an error occurs (like a parse error) just return the current time.
                return DateTime.Now;
            }
        }
    }
}