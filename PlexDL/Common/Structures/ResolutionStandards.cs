using PlexDL.Common.Logging;
using System;

namespace PlexDL.Common.Structures
{
    public static class ResolutionStandards
    {
        public static string[][] HeightStandards { get; } = {
            new[] { "480", "SD NTSC" },
            new[] { "576", "SD PAL"  },
            new[] { "720","HD"       },
            new[] { "1080","FHD"     },
            new[] { "1440","QHD"     },
            new[] { "2160", "4K UHD" },
            new[] { "4320", "8K UHD" }
        };

        public static string[][] PlexFramerates { get; } = {
            new[] { "ntsc", "29.97" },
            new[] { "pal", "25.00"  },
            new[] { "24p", "24.00"  },
            new[] { "60p", "60.00"  },
            new[] { "50p", "50.00"  },
            new[] { "25p", "25.00"  }
        };

        //for organisational reasons
        public static string[][] RegionalFramerates { get; } = {
            new[] { "ntsc", "29.97" },
            new[] { "pal", "25.00"  }
        };

        public static string FpsPSuffix(string fps)
        {
            var suffixed = "";
            try
            {
                if (double.TryParse(fps, out var fpsNum))
                {
                    fpsNum = Math.Round(fpsNum);
                    suffixed = fpsNum + "p";
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "FpsFromStdError");
            }
            return suffixed;
        }

        public static string FpsFromPlexStd(string std, bool includeSuffix = true)
        {
            var fps = "29.97"; //default NTSC
            try
            {
                foreach (var s in PlexFramerates)
                {
                    if (!string.Equals(s[0], std)) continue;

                    fps = s[1];
                    break;
                }

                if (includeSuffix)
                    fps = FullFpsSuffix(fps);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "FpsFromStdError");
            }
            return fps;
        }

        public static string FullFpsSuffix(string fps)
        {
            var region = RegionalStdFromFps(fps);

            string suffixed;
            if (string.IsNullOrEmpty(region))
                suffixed = fps + "fps (" + FpsPSuffix(fps) + ")";
            else
                suffixed = fps + "fps (" + region + ")";

            return suffixed;
        }

        public static string RegionalStdFromFps(string fps)
        {
            var std = "";
            foreach (var s in RegionalFramerates)
            {
                if (!string.Equals(s[1], fps)) continue;

                std = s[0].ToUpper();
                break;
            }
            return std;
        }

        public static string StdFromHeight(string height)
        {
            var std = "";
            foreach (var s in HeightStandards)
            {
                if (!string.Equals(s[0], height)) continue;

                std = s[1];
                break;
            }
            return std;
        }
    }
}