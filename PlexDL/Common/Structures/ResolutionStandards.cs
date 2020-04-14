namespace PlexDL.Common.Structures
{
    public static class ResolutionStandards
    {
        public static string[][] Standards { get; set; } = new string[][] {
                new string[2] { "480", "SD NTSC" },
                new string[2] { "576", "SD PAL"  },
                new string[2] { "720","HD"       },
                new string[2] { "1080","FHD"     },
                new string[2] { "1440","QHD"     },
                new string[2] { "2160", "4K UHD" },
                new string[2] { "4320", "8K UHD" }
            };
    }
}