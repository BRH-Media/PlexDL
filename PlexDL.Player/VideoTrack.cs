using System;
using System.Globalization;
using System.Text;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media video track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class VideoTrack : HideObjectMembers
    {
        #region Fields (Video Track Class)

        // Video Subtypes (FOURCC) Lookup Table
        internal static string[] _videoSubTypes =
        {
            "H.264",
            "H.264",
            "H.265",
            "H.265",
            "MPEG-1",
            "AV1",
            "MPEG-4 Part 2",
            "MPEG-4 Part 2",
            "VP8",
            "VP9",
            "WM Video 9",
            "WM Video 9 AP",
            "DVC PRO 25",
            "DVC PRO 50",
            "DVC/DV",
            "DVC PRO 100",
            "HD-DVCR",
            "SDL-DVCR",
            "SD-DVCR",
            "H.263",
            "HEVS",
            "Motion JPEG",
            "MPEG 4 Version 3",
            "MPEG 4 Version 1",
            "WM Screen Version 1",
            "WM Video 9 Screen",
            "WM Video Version 7",
            "WM Video 8",
            "YUV 4:2:0"
        };
        internal static int[] _videoFOURCCs =
        {
            0x34363248, // H.264
            0x3f40f4f0, // H.264
            0x35363248, // H.265
            0x43564548, // HEVC (H.265)
            0x3147504d, // MPEG-1
            0x31305641, // AV1
            0x5634504d, // MPEG-4 Part 2
            0x3253344d, // MPEG-4 Part 2
            0x30385056, // VP8
            0x30395056, // VP9
            0x33564d57, // Windows Media Video 9
            0x31435657, // Windows Media Video 9 Advanced Profile
            0x35327664, // DVCPRO 25
            0x30357664, // DVCPRO 50
            0x20637664, // DVC/DV
            0x31687664, // DVCPRO 100
            0x64687664, // HD-DVCR
            0x64737664, // SDL-DVCR
            0x6c737664, // SD-DVCR
            0x33363248, // H.263
            0x53564548, // HEVS
            0x47504a4d, // Motion JPEG
            0x3334504d, // MPEG 4 Version 3
            0x5334504d, // MPEG 4 Version 1
            0x3153534d, // Windows Media Screen Version 1
            0x3253534d, // Windows Media Video 9 Screen
            0x31564d57, // Windows Media Video Version 7
            0x32564d57, // Windows Media Video 8
            0x4f303234  // YUV 4:2:0
        };

        internal Guid   _mediaType;
        internal string _name;
        internal string _language;
        internal int    _bitRate;
        internal float  _frameRate;
        internal int    _width;
        internal int    _height;

        #endregion

        
        internal VideoTrack() { }

        /// <summary>
        /// Gets the media type (MF GUID) of the track.
        /// <br/>See the Media Foundation documentation for more information.
        /// </summary>
        public Guid MediaType { get { return _mediaType; } }

        /// <summary>
        /// Returns a string representing the base video type (eg "H.264") of this track.
        /// </summary>
        public string VideoType
        {
            get
            {
                string videoType = string.Empty;
                try
                {
                    int FOURCC = BitConverter.ToInt32(_mediaType.ToByteArray(), 0);
                    for (int i = 0; i < _videoFOURCCs.Length; i++)
                    {
                        if (FOURCC == _videoFOURCCs[i])
                        {
                            videoType = _videoSubTypes[i];
                            break;
                        }
                    }
                }
                catch { /* ignored */ }
                return videoType;
            }
        }

        /// <summary>
        /// Gets the name of the track.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the language of the track, a 3 letter ISO 639.2 code.
        /// </summary>
        public string Language { get { return _language; } }

        /// <summary>
        /// Gets the average video bitrate (kb/s - kilobits per second) of the track.
        /// </summary>
        public int BitRate { get { return _bitRate; } }

        /// <summary>
        /// Gets the video frame rate (fps - frames per second) of the track.
        /// </summary>
        public float FrameRate { get { return _frameRate; } }

        /// <summary>
        /// Gets the video width of the track.
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// Gets the video height of the track.
        /// </summary>
        public int Height { get { return _height; } }

        /// <summary>
        /// Returns a string with a detailed description of the track.
        /// </summary>
        public override string ToString()
        {
            StringBuilder trackInfo = new StringBuilder(_name, 128);
            trackInfo.Append(" - ");

            if (_mediaType != null)
            {
                try
                {
                    int FOURCC = BitConverter.ToInt32(_mediaType.ToByteArray(), 0);
                    for (int i = 0; i < _videoFOURCCs.Length; i++)
                    {
                        if (FOURCC == _videoFOURCCs[i])
                        {
                            trackInfo.Append(_videoSubTypes[i]).Append(", ");
                            break;
                        }
                    }
                }
                catch { /* ignored */ }
            }

            trackInfo.Append(_width).Append(" x ").Append(_height).Append(", ").Append(_frameRate.ToString("0.##")).Append(" fps");
            if (_bitRate > 0) trackInfo.Append(", ").Append(_bitRate).Append(" kb/s");

            if (_language != null && _language != string.Empty)
            {
                try
                {
                    trackInfo.Append(" - ");

                    CultureInfo cultureInfo = new CultureInfo(_language);
                    if (cultureInfo != CultureInfo.CurrentCulture) trackInfo.Append(cultureInfo.NativeName).Append(" (").Append(cultureInfo.DisplayName).Append(')');
                    else trackInfo.Append(cultureInfo.DisplayName);
                }
                catch { trackInfo.Append(_language); }
            }

            return trackInfo.ToString();
        }
    }
}