using PlexDL.AltoHTTP.Enums;
using System;

namespace PlexDL.AltoHTTP.Classes
{
    internal static class SizeUnitConverter
    {
        public static string ConvertBestScaledSize(this long bytes)
        {
            var unit = 1024;
            var inBytes = bytes < unit;
            var inKb = bytes < unit * unit;
            var inMb = bytes < unit * unit * unit;
            if (inBytes)
                return bytes + " bytes";
            else if (inKb)
                return (bytes / 1024d).ToString("0.00") + " kb";
            else if (inMb)
                return (bytes / 1024d / 1024).ToString("0.00") + " mb";
            else
                return (bytes / 1024d / 1024 / 1024).ToString("0.00") + " gb";
        }

        public static double ConvertMemorySize(this long size, FromTo fromTo)
        {
            var degree = (int)fromTo;
            if (degree < 0)
            {
                var divide = Math.Pow(1024, -degree);
                return size / divide;
            }
            else
            {
                var multiplier = Math.Pow(1024, degree);
                return size * multiplier;
            }
        }
    }
}