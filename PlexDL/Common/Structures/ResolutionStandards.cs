using System;

namespace PlexDL.Common.Structures
{
    public static class ResolutionStandards
    {
        public static string[][] HeightStandards { get; } = new string[][]
        {
            new string[2] { "480", "SD NTSC" },
            new string[2] { "576", "SD PAL"  },
            new string[2] { "720","HD"       },
            new string[2] { "1080","FHD"     },
            new string[2] { "1440","QHD"     },
            new string[2] { "2160", "4K UHD" },
            new string[2] { "4320", "8K UHD" }
        };

        public static string[][] PlexFramerates { get; } = new string[][]
        {
            new string[2] { "ntsc", "29.97" },
            new string[2] { "pal", "25.00" },
            new string[2] { "24p", "24.00" },
            new string[2] { "60p", "60.00" },
            new string[2] { "50p", "50.00" },
            new string[2] { "25p", "25.00" }
        };

        //for organisational reasons
        public static string[][] RegionalFramerates { get; } = new string[][]
        {
            new string[2] { "ntsc", "29.97" },
            new string[2] { "pal", "25.00" }
        };

        public static string FpsPSuffix(string fps)
        {
            string suffixed = "";
            try
            {
                if (double.TryParse(fps, out double fpsNum))
                {
                    fpsNum = Math.Round(fpsNum);
                    suffixed = fpsNum + "p";
                }
            }
            catch (Exception ex)
            {
                Logging.LoggingHelpers.RecordException(ex.Message, "FpsFromStdError");
            }
            return suffixed;
        }

        public static string FpsFromPlexStd(string std, bool includeSuffix = true)
        {
            string fps = "29.97"; //default NTSC
            try
            {
                foreach (string[] s in PlexFramerates)
                {
                    if (string.Equals(s[0], std))
                    {
                        fps = s[1];
                        break;
                    }
                }

                if (includeSuffix)
                    fps = FullFpsSuffix(fps);
            }
            catch (Exception ex)
            {
                Logging.LoggingHelpers.RecordException(ex.Message, "FpsFromStdError");
            }
            return fps;
        }

        public static string FullFpsSuffix(string fps)
        {
            string suffixed = "";
            string region = RegionalStdFromFps(fps);

            if (string.IsNullOrEmpty(region))
                suffixed = fps + "fps (" + FpsPSuffix(fps) + ")";
            else
                suffixed = fps + "fps (" + region + ")";

            return suffixed;
        }

        public static string RegionalStdFromFps(string fps)
        {
            string std = "";
            foreach (string[] s in RegionalFramerates)
            {
                if (string.Equals(s[1], fps))
                {
                    std = s[0].ToUpper();
                    break;
                }
            }
            return std;
        }

        public static string StdFromHeight(string height)
        {
            string std = "";
            foreach (string[] s in HeightStandards)
            {
                if (string.Equals(s[0], height))
                {
                    std = s[1];
                    break;
                }
            }
            return std;
        }
    }
}