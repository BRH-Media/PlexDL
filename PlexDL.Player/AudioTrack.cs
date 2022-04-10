using System;
using System.Globalization;
using System.Text;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media audio track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class AudioTrack : HideObjectMembers
    {
        #region Fields (Audio Track Class)

        // Audio Subtypes (FOURCC) Lookup Table
        internal static string[] _audioSubTypes =
        {
            "AAC",
            "MP3",
            "AC3",
            "AC3",
            "WM Audio 9 Pro",
            "PCM",
            "DTS",
            "AMR NB",
            "AMR WB",
            "AMR WP",
            "MPEG",
            "Opus",
            "Float",
            "FLAC",
            "Raw AAC1",
            "ADTS",
            "ALAC",
            "AC3 S/PDIF",
            "DRM",
            "MSP1",
            "WM Audio 9 Pro S/PDIF",
            "WM Audio 9 Lossless",
            "WM Audio 8",
        };
        internal static int[] _audioFOURCCs =
        {
            0x1610, // AAC
            0x0055, // MP3
            0x2000, // AC3
            unchecked((int)0xe06d802c), // AC3
            0x0162, // WMAudioV9
            1,      // PCM
            0x0008, // DTS
            0x7361, // AMR NB
            0x7362, // AMR WB
            0x7363, // AMR WP
            0x0050, // MPEG
            0x704F, // Opus
            0x0003, // Float
            0xF1AC, // FLAC
            0x00FF, // Raw AAC1
            0x1600, // ADTS
            0x6C61, // ALAC
            0x0092, // AC3 SPDIF
            0x0009, // DRM
            0x000A, // MSP1
            0x0164, // WMASPDIF
            0x0163, // WMAudio Lossless
            0x0161, // WMAudioV8
        };

        internal Guid   _mediaType;
        internal string _name;
        internal string _language;
        internal int    _channelCount;
        internal int    _sampleRate;
        internal int    _bitDepth;
        internal int    _bitRate;

        #endregion

        
        internal AudioTrack() { }

        /// <summary>
        /// Gets the media type (GUID) of the track.
        /// <br/>See the Media Foundation documentation for more information.
        /// </summary>
        public Guid MediaType { get { return _mediaType; } }

        /// <summary>
        /// Returns a string representing the audio type (eg "AAC") of this track.
        /// </summary>
        public string AudioType
        {
            get
            {
                string audioType = string.Empty;
                try
                {
                    int FOURCC = BitConverter.ToInt32(_mediaType.ToByteArray(), 0);
                    for (int i = 0, count = _audioFOURCCs.Length; i < count; i++)
                    {
                        if (FOURCC == _audioFOURCCs[i])
                        {
                            audioType = _audioSubTypes[i];
                            break;
                        }
                    }
                }
                catch { /* ignored */ }
                return audioType;
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
        /// Gets the number of channels in the track.
        /// </summary>
        public int ChannelCount { get { return _channelCount; } }

        /// <summary>
        /// Gets the sample rate (samples per second) of the track.
        /// </summary>
        public int SampleRate { get { return _sampleRate; } }

        /// <summary>
        /// Gets the bit depth (bits per sample) of the track,
        /// <br/>or 0 (zero) if not available.
        /// </summary>
        public int BitDepth { get { return _bitDepth; } }

        /// <summary>
        /// Gets the average bitrate (kb/s - kilobits per second) of the track,
        /// <br/>or 0 (zero) if not available.
        /// </summary>
        public int BitRate { get { return _bitRate; } }

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
                    for (int i = 0, count = _audioFOURCCs.Length; i < count;  i++)
                    {
                        if (FOURCC == _audioFOURCCs[i])
                        {
                            trackInfo.Append(_audioSubTypes[i]).Append(", ");
                            break;
                        }
                    }
                }
                catch { /* ignored */ }
            }
            
            //trackInfo.Append(", ").Append(_channelCount).Append(_channelCount == 1 ? " channel, " : " channels, ");
            //if (_bitDepth > 0) trackInfo.Append(_bitDepth).Append(" bits, ");
            //trackInfo.Append(_sampleRate).Append(" hz, ").Append(_bitRate).Append(" kb/s");

            switch (_channelCount)
            {
                case 1:
                    trackInfo.Append("mono");
                    break;
                case 2:
                    trackInfo.Append("stereo");
                    break;
                case 6:
                    trackInfo.Append("5.1 surround");
                    break;
                case 7:
                    trackInfo.Append("6.1 surround");
                    break;
                case 8:
                    trackInfo.Append("7.1 surround");
                    break;
                default:
                    trackInfo.Append(_channelCount).Append(" channels");
                    break;
            }

            trackInfo.Append(", ");
            if (_bitDepth > 0) trackInfo.Append(_bitDepth).Append(" bits, ");
            trackInfo.Append(_sampleRate).Append(" Hz, ").Append(_bitRate).Append(" kb/s");

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