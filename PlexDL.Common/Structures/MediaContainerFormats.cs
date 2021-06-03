using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace PlexDL.Common.Structures
{
    public static class MediaContainerFormats
    {
        public static List<string[]> ContainerDefinitions { get; } = new List<string[]>
        {
            new[]
            {
                "mkv", "Matroska"
            },
            new[]
            {
                "mp4", "MPEG-4"
            },
            new[]
            {
                "mp3", "MPEG-3 Audio"
            },
            new[]
            {
                "ts", "MPEG-2 Transport Stream"
            },
            new[]
            {
                "ogg", "Ogg Vorbis Audio"
            },
            new[]
            {
                "ogv", "Ogg Vorbis Video"
            },
            new[]
            {
                "avi", "Audio Video Interchange"
            },
            new[]
            {
                "wmv","Windows Media Video"
            },
            new[]
            {
                "wma", "Windows Media Audio"
            },
            new[]
            {
                "srt","SubRip"
            },
            new[]
            {
                "vtt", "WebVTT"
            },
            new[]
            {
                "aac", "MPEG-2 AAC"
            },
            new[]
            {
                "mov","QuickTime Movie"
            },
            new[]
            {
                "wav","Waveform Audio"
            },
            new[]
            {
                "flac",""
            },
            new[]
            {
                "m4a","MPEG-4 Audio"
            },
            new[]
            {
                "asf", "Advanced Systems Format"
            }
        };

        public static string FormatToDescription(string format)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(format))

                    //go through each container definition
                    foreach (var c in ContainerDefinitions.Where(c => string.Equals(c[0], format, StringComparison.CurrentCultureIgnoreCase)))

                        //return corresponding description
                        return c[1];
            }
            catch (Exception ex)
            {
                //log error
                LoggingHelpers.RecordException(ex.Message, @"MediaContainerDescriptionError");
            }

            //default
            return format;
        }
    }
}