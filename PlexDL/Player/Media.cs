using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Media methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Media : HideObjectMembers
    {
        #region Fields (Media Class)

        private Player _base;

        // Media album art information
        private Image _tagImage;

        private DirectoryInfo _directoryInfo;

        private string[] _searchKeyWords =
        {
            "*front*", "*cover*"
        }; // , "*albumart*large*" };

        private string[] _searchExtensions =
        {
            ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".tiff"
        };

        // Wma Guid
        //private static Guid ASF_Header_Guid = new Guid("75B22630-668E-11CF-A6D9-00AA0062CE6C");
        //private static Guid ASF_Content_Description_Guid = new Guid("75B22633-668E-11CF-A6D9-00AA0062CE6C");
        //private static Guid ASF_Extended_Content_Description_Guid = new Guid("D2D0A440-E307-11D2-97F0-00A0C95EA850");
        //private static Guid ASF_Header_Extension_Object_Guid = new Guid("5FBF03B5-A92E-11CF-8EE3-00C00C205365");
        //private static Guid ASF_Metadata_Library_Object = new Guid("44231C94-9498-49D1-A141-1D134E457054");

        #endregion Fields (Media Class)

        internal Media(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets the natural length (duration) of the playing media. See also: Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return TimeSpan.FromTicks(_base._mediaLength);
            }
        }

        /// <summary>
        /// Gets the duration of the playing media from its start time to its stop time. See also: Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return _base._stopTime == 0
                ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime)
                : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
            }
        }

        /// <summary>
        /// Returns the duration of the specified part of the playing media.
        /// </summary>
        /// <param name="part">Specifies the part of the playing media whose duration is to be obtained.</param>
        public TimeSpan GetDuration(MediaPart part)
        {
            _base._lastError = Player.NO_ERROR;

            if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;

            switch (part)
            {
                case MediaPart.BeginToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength);
                //break;

                case MediaPart.StartToStop:
                    return _base._stopTime == 0
                    ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime)
                    : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
                //break;

                case MediaPart.FromStart:
                    return TimeSpan.FromTicks(_base.PositionX - _base._startTime);
                //break;

                case MediaPart.ToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength - _base.PositionX);
                //break;

                case MediaPart.ToStop:
                    return _base._stopTime == 0
                    ? TimeSpan.FromTicks(_base._mediaLength - _base.PositionX)
                    : TimeSpan.FromTicks(_base._stopTime - _base.PositionX);
                //break;

                //case MediaLength.FromBegin:
                default:
                    return (TimeSpan.FromTicks(_base.PositionX));
                    //break;
            }
        }

        /// <summary>
        /// Returns (part of) the file (or webcam) name of the playing media.
        /// </summary>
        /// <param name="part">Specifies the part of the file name to return.</param>
        public string GetName(MediaName part)
        {
            string mediaName = string.Empty;
            _base._lastError = Player.NO_ERROR;

            if (!_base._fileMode)
            {
                if (part == MediaName.FileName || part == MediaName.FileNameWithoutExtension) mediaName = _base._fileName;
            }
            else if (_base._playing)
            {
                try
                {
                    switch (part)
                    {
                        case MediaName.FileName:
                            mediaName = Path.GetFileName(_base._fileName);
                            break;

                        case MediaName.DirectoryName:
                            mediaName = Path.GetDirectoryName(_base._fileName);
                            break;

                        case MediaName.PathRoot:
                            mediaName = Path.GetPathRoot(_base._fileName);
                            break;

                        case MediaName.Extension:
                            mediaName = Path.GetExtension(_base._fileName);
                            break;

                        case MediaName.FileNameWithoutExtension:
                            mediaName = Path.GetFileNameWithoutExtension(_base._fileName);
                            break;

                        default: // case MediaName.FullPath:
                            mediaName = _base._fileName;
                            break;
                    }
                }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
            }

            return mediaName;
        }

        /// <summary>
        /// Gets the number of audio tracks in the playing media. See also: Player.Media.GetAudioTracks.
        /// </summary>
        public int AudioTrackCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioTrackCount;
            }
        }

        /// <summary>
        /// Returns a list of the audio tracks in the playing media. Returns null if no audio tracks are present. See also: Player.Media.AudioTrackCount.
        /// </summary>
        public AudioTrack[] GetAudioTracks()
        {
            _base._lastError = Player.NO_ERROR;

            AudioTrack[] tracks = null;
            int count = _base._audioTrackCount;
            if (count > 0)
            {
                tracks = new AudioTrack[count];
                for (int i = 0; i < count; i++)
                {
                    AudioTrack track = new AudioTrack();
                    track._mediaType = _base._audioTracks[i].MediaType;
                    track._name = _base._audioTracks[i].Name;
                    track._language = _base._audioTracks[i].Language;
                    track._channelCount = _base._audioTracks[i].ChannelCount;
                    track._samplerate = _base._audioTracks[i].Samplerate;
                    track._bitdepth = _base._audioTracks[i].Bitdepth;
                    track._bitrate = _base._audioTracks[i].Bitrate;
                    tracks[i] = track;
                }
            }

            return tracks;
        }

        /// <summary>
        /// Gets the number of video tracks in the playing media. See also: Player.Media.GetVideoTracks.
        /// </summary>
        public int VideoTrackCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._videoTrackCount;
            }
        }

        /// <summary>
        /// Returns a list of the video tracks in the playing media. Returns null if no video tracks are present. See also: Player.Media.VideoTrackCount.
        /// </summary>
        public VideoTrack[] GetVideoTracks()
        {
            _base._lastError = Player.NO_ERROR;

            VideoTrack[] tracks = null;
            int count = _base._videoTrackCount;
            if (count > 0)
            {
                tracks = new VideoTrack[count];
                for (int i = 0; i < count; i++)
                {
                    VideoTrack track = new VideoTrack();
                    track._mediaType = _base._videoTracks[i].MediaType;
                    track._name = _base._videoTracks[i].Name;
                    track._language = _base._videoTracks[i].Language;
                    track._frameRate = _base._videoTracks[i].FrameRate;
                    track._width = _base._videoTracks[i].SourceWidth;
                    track._height = _base._videoTracks[i].SourceHeight;
                    tracks[i] = track;
                }
            }

            return tracks;
        }

        /// <summary>
        /// Gets the original size of the video image of the playing media.
        /// </summary>
        public Size VideoSourceSize
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo ? _base._videoSourceSize : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the video frame rate of the playing media, in frames per second.
        /// </summary>
        public float VideoFrameRate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo ? _base._videoFrameRate : 0;
            }
        }

        /// <summary>
        /// Gets or sets the playback (repeat) start time of the playing media. The start time can also be set with the Player.Play instruction.
        /// </summary>
        public TimeSpan StartTime
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return TimeSpan.FromTicks(_base._startTime);
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (!_base._playing || !_base._fileMode) return;

                long newStart = value.Ticks;

                if (_base._startTime == newStart) return;
                if ((_base._stopTime != 0 && newStart >= _base._stopTime) || newStart >= _base._mediaLength)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    return;
                }

                _base._startTime = newStart;
                if (newStart > _base.PositionX) _base.PositionX = newStart;
                if (_base._mediaStartStopTimeChanged != null) _base._mediaStartStopTimeChanged(_base, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the playback (repeat) stop time of the playing media. The stop time can also be set with the Player.Play instruction.
        /// TimeSpan.Zero or 00:00:00 = natural end of media.
        /// </summary>
        public TimeSpan StopTime
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return TimeSpan.FromTicks(_base._stopTime);
            }
            set
            {
                _base._lastError = Player.NO_ERROR;

                if (!_base._playing || !_base._fileMode) return;

                long newStop = value.Ticks;

                if (_base._stopTime == newStop) return;
                if ((newStop != 0 && newStop < _base._startTime) || newStop >= _base._mediaLength)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                    return;
                }

                _base._stopTime = newStop;
                _base.AV_UpdateTopology();
                if (_base._mediaStartStopTimeChanged != null) _base._mediaStartStopTimeChanged(_base, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Returns the metadata properties of the playing media (using ImageSource.MediaOrFolder).
        /// </summary>
        public Metadata GetMetadata()
        {
            if (_base._fileMode && _base._playing)
            {
                return GetMetadata(_base._fileName, ImageSource.MediaOrFolder);
            }

            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return new Metadata();
        }

        /// <summary>
        /// Returns the metadata properties of the playing media.
        /// </summary>
        /// <param name="imageSource">A value indicating whether and where an image related to the media should be obtained.</param>
        public Metadata GetMetadata(ImageSource imageSource)
        {
            if (_base._fileMode && _base._playing)
            {
                return GetMetadata(_base._fileName, imageSource);
            }

            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            ;
            return new Metadata();
        }

        /// <summary>
        /// Returns the metadata properties of the specified media file.
        /// </summary>
        /// <param name="fileName">The path and name of the file whose metadata properties are to be obtained.</param>
        /// <param name="imageSource">A value indicating whether and where an image related to the media should be obtained.</param>
        /// <returns></returns>
        public Metadata GetMetadata(string fileName, ImageSource imageSource)
        {
            Metadata tagInfo;
            _base._lastError = Player.NO_ERROR;

            if (fileName == null || fileName == string.Empty)
            {
                _base._lastError = HResult.E_INVALIDARG;
                return new Metadata();
            }

            if (!new Uri(fileName).IsFile)
            {
                tagInfo = new Metadata();
                {
                    try
                    {
                        tagInfo._title = Path.GetFileNameWithoutExtension(fileName);
                        tagInfo._album = fileName;
                    }
                    catch
                    {
                        /* ignore */
                    }
                }
                return tagInfo;
            }

            tagInfo = GetMediaTags(fileName, imageSource);

            try
            {
                // Get info from file path
                if (tagInfo._artist == null || tagInfo._artist.Length == 0) tagInfo._artist = Path.GetFileName(Path.GetDirectoryName(fileName));
                if (tagInfo._title == null || tagInfo._title.Length == 0) tagInfo._title = Path.GetFileNameWithoutExtension(fileName);

                // Get album art image (with certain values of 'imageSource')
                if (imageSource == ImageSource.FolderOrMedia || imageSource == ImageSource.FolderOnly ||
                    (imageSource == ImageSource.MediaOrFolder && tagInfo._image == null))
                {
                    GetMediaImage(fileName);
                    if (_tagImage != null) // null image not to replace image retrieved from media file with FolderOrMedia
                    {
                        tagInfo._image = _tagImage;
                        _tagImage = null;
                    }
                }
            }
            catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

            return tagInfo;
        }

        private Metadata GetMediaTags(string fileName, ImageSource imageSource)
        {
            Metadata tagInfo = new Metadata();
            IMFSourceResolver sourceResolver;
            IMFMediaSource mediaSource = null;
            IPropertyStore propStore = null;
            PropVariant propVariant = new PropVariant();

            HResult result = MFExtern.MFCreateSourceResolver(out sourceResolver);
            if (result == Player.NO_ERROR)
            {
                try
                {
                    MFObjectType objectType;
                    object source;

                    result = sourceResolver.CreateObjectFromURL(fileName, MFResolution.MediaSource, null, out objectType, out source);
                    if (result == Player.NO_ERROR)
                    {
                        mediaSource = (IMFMediaSource)source;

                        object store;
                        result = MFExtern.MFGetService(mediaSource, MFServices.MF_PROPERTY_HANDLER_SERVICE, typeof(IPropertyStore).GUID, out store);
                        if (result == Player.NO_ERROR)
                        {
                            propStore = (IPropertyStore)store;

                            // Artist
                            result = propStore.GetValue(PropertyKeys.PKEY_Music_Artist, propVariant);
                            tagInfo._artist = propVariant.GetString();
                            if (string.IsNullOrEmpty(tagInfo._artist))
                            {
                                propStore.GetValue(PropertyKeys.PKEY_Music_AlbumArtist, propVariant);
                                tagInfo._artist = propVariant.GetString();
                            }

                            // Title
                            propStore.GetValue(PropertyKeys.PKEY_Title, propVariant);
                            tagInfo._title = propVariant.GetString();

                            // Album
                            propStore.GetValue(PropertyKeys.PKEY_Music_AlbumTitle, propVariant);
                            tagInfo._album = propVariant.GetString();

                            // Genre
                            propStore.GetValue(PropertyKeys.PKEY_Music_Genre, propVariant);
                            tagInfo._genre = propVariant.GetString();

                            // Duration
                            propStore.GetValue(PropertyKeys.PKEY_Media_Duration, propVariant);
                            tagInfo._duration = TimeSpan.FromTicks((long)propVariant.GetULong());

                            // TrackNumber
                            propStore.GetValue(PropertyKeys.PKEY_Music_TrackNumber, propVariant);
                            tagInfo._trackNumber = (int)propVariant.GetUInt();

                            // Year
                            propStore.GetValue(PropertyKeys.PKEY_Media_Year, propVariant);
                            tagInfo._year = propVariant.GetUInt().ToString();

                            // Image
                            if (imageSource != ImageSource.None && imageSource != ImageSource.FolderOnly)
                            {
                                propStore.GetValue(PropertyKeys.PKEY_ThumbnailStream, propVariant);
                                if (propVariant.ptr != null)
                                {
                                    IStream stream = (IStream)Marshal.GetObjectForIUnknown(propVariant.ptr);

                                    STATSTG streamInfo;
                                    stream.Stat(out streamInfo, STATFLAG.NoName);

                                    int streamSize = (int)streamInfo.cbSize;
                                    if (streamSize > 0)
                                    {
                                        byte[] buffer = new byte[streamSize];
                                        stream.Read(buffer, streamSize, IntPtr.Zero);

                                        MemoryStream ms = new MemoryStream(buffer, false);
                                        Image image = Image.FromStream(ms);

                                        tagInfo._image = new Bitmap(image);

                                        image.Dispose();
                                        ms.Dispose();
                                    }

                                    Marshal.ReleaseComObject(streamInfo);
                                    Marshal.ReleaseComObject(stream);
                                }
                            }
                        }
                    }
                }
                //catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
                catch
                {
                    /* ignore */
                }
            }

            if (sourceResolver != null) Marshal.ReleaseComObject(sourceResolver);
            if (mediaSource != null) Marshal.ReleaseComObject(mediaSource);
            if (propStore != null) Marshal.ReleaseComObject(propStore);
            propVariant.Dispose();

            return tagInfo;
        }

        // Get mp3 information string help function
        //private string GetMp3String(FileStream fs, byte[] buffer, int frameSize)
        //{
        //    string result;

        //    if (frameSize > buffer.Length) buffer = new byte[frameSize];
        //    fs.Read(buffer, 0, frameSize);
        //    switch (buffer[1])
        //    {
        //        case 0xFF:
        //            result = Encoding.Unicode.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //        case 0xFE:
        //            result = Encoding.BigEndianUnicode.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //        default:
        //            result = Encoding.Default.GetString(buffer, 1, frameSize - 1).TrimEnd('\0');
        //            break;
        //    }

        //    return result.Trim();
        //}

        // Get media information image help function
        private void GetMediaImage(string fileName)
        {
            _directoryInfo = new DirectoryInfo(Path.GetDirectoryName(fileName));
            string searchFileName = Path.GetFileNameWithoutExtension(fileName);
            string searchDirectoryName = _directoryInfo.Name;

            // 1. search using the file name
            if (!SearchMediaImage(searchFileName))
            {
                // 2. search using the directory name
                if (!SearchMediaImage(searchDirectoryName))
                {
                    // 3. search using keywords
                    int i = 0;
                    bool result;
                    do result = SearchMediaImage(_searchKeyWords[i++]);
                    while (!result && i < _searchKeyWords.Length);

                    if (!result)
                    {
                        // 4. find largest file
                        SearchMediaImage("*");
                    }
                }
            }

            _directoryInfo = null;
        }

        // Get media image help function
        private bool SearchMediaImage(string searchName)
        {
            if (searchName.EndsWith(@":\")) return false; // root directory - no folder name (_searchDirectoryName, for example C:\)

            string imageFile = string.Empty;
            long length = 0;
            bool found = false;

            for (int i = 0; i < _searchExtensions.Length; i++)
            {
                FileInfo[] filesFound = _directoryInfo.GetFiles(searchName + _searchExtensions[i]);

                if (filesFound.Length > 0)
                {
                    for (int j = 0; j < filesFound.Length; j++)
                    {
                        if (filesFound[j].Length > length)
                        {
                            length = filesFound[j].Length;
                            imageFile = filesFound[j].FullName;
                            found = true;
                        }
                    }
                }
            }

            if (found) _tagImage = Image.FromFile(imageFile);
            return found;
        }

        ///// <summary>
        ///// Returns the path to a new file, created from the specified embedded resource and with the specified file name, in the system's temporary folder for use with the Player.Play methods.
        ///// </summary>
        ///// <param name="resource">The embedded resource (for example, Properties.Resources.MyMedia) to save to a new file in the system's temporary folder.</param>
        ///// <param name="fileName">The file name (for example, "MyMedia.mp4") to be used for the new file in the system's temporary folder.</param>
        //public string ResourceToFile(byte[] resource, string fileName)
        //{
        //    return _base.AV_ResourceToFile(resource, fileName);
        //}
    }
}