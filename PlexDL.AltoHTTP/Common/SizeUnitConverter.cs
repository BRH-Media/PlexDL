using PlexDL.AltoHTTP.Enums;
using System;

namespace PlexDL.AltoHTTP.Common
{
    internal static class SizeUnitConverter
    {
        public static string ConvertBestScaledSize(this long bytes)
        {
            const int unit = 1024;
            var inBytes = bytes < unit;
            var inKb = bytes < unit * unit;
            var inMb = bytes < unit * unit * unit;
            if (inBytes)
                return bytes + " bytes";
            if (inKb)
                return (bytes / 1024d).ToString("0.00") + " kb";
            if (inMb)
                return (bytes / 1024d / 1024).ToString("0.00") + " mb";
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

            var multiplier = Math.Pow(1024, degree);
            return size * multiplier;
        }
    }
}