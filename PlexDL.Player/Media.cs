using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Media methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Media : HideObjectMembers
    {
        #region Fields (Media Class)

        private const int NO_ERROR = 0;

        private Player _base;

        // Media album art information
        private Image _tagImage;
        private DirectoryInfo _directoryInfo;
        private string[] _searchKeyWords = { "*front*", "*cover*" }; // , "*albumart*large*" };
        private string[] _searchExtensions = { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".tiff", ".tif", ".jfif" };

        #endregion

        internal Media(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating the source type of the playing media,
        /// <br/>such as a local file or a webcam.
        /// </summary>
        public MediaSourceType SourceType
        {
            get
            {
                _base._lastError = NO_ERROR;
                MediaSourceType source = MediaSourceType.None;

                if (_base._playing) source = _base.AV_GetSourceType();
                return source;
            }
        }

        /// <summary>
        /// Gets a value indicating the source category of the playing media,
        /// <br/>such as local file or local capture device.
        /// </summary>
        public MediaSourceCategory SourceCategory
        {
            get
            {
                _base._lastError = NO_ERROR;
                MediaSourceCategory source = MediaSourceCategory.None;

                if (_base._playing) source = _base.AV_GetSourceCategory();
                return source;
            }
        }

        /// <summary>
        /// Gets the natural length (duration) of the playing media.
        /// <br/>See also: Player.Media.Duration and Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return TimeSpan.FromTicks(_base._mediaLength);
            }
        }

        /// <summary>
        /// Gets the duration of the playing media from the (adjustable) start time to the stop time.
        /// <br/>See also: Player.Media.Length and Player.Media.GetDuration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;
                return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime) : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
            }
        }

        /// <summary>
        /// Returns the duration of the specified part of the playing media.
        /// <br/>See also: Player.Media.Length and Player.Media.Duration.
        /// </summary>
        /// <param name="part">Specifies the part of the playing media whose duration is to be obtained.</param>
        public TimeSpan GetDuration(MediaPart part)
        {
            _base._lastError = NO_ERROR;

            if (!_base._playing || !_base._fileMode) return TimeSpan.Zero;

            switch (part)
            {
                case MediaPart.BeginToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength);
                //break;

                case MediaPart.StartToStop:
                    return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base._startTime) : TimeSpan.FromTicks(_base._stopTime - _base._startTime);
                //break;

                case MediaPart.FromStart:
                    return TimeSpan.FromTicks(_base.PositionX - _base._startTime);
                //break;

                case MediaPart.ToEnd:
                    return TimeSpan.FromTicks(_base._mediaLength - _base.PositionX);
                //break;

                case MediaPart.ToStop:
                    return _base._stopTime == 0 ? TimeSpan.FromTicks(_base._mediaLength - _base.PositionX) : TimeSpan.FromTicks(_base._stopTime - _base.PositionX);
                //break;

                //case MediaLength.FromBegin:
                default:
                    return (TimeSpan.FromTicks(_base.PositionX));
                //break;
            }
        }

        /// <summary>
        /// Returns the specified part of the file or device name of the playing media.
        /// </summary>
        /// <param name="part">Specifies the part of the name to return.</param>
        public string GetName(MediaName part)
        {
            string mediaName = string.Empty;
            _base._lastError = NO_ERROR;

            if (!_base._fileMode) // && !_base._liveStreamMode)
            {
                if (part == MediaName.DeviceName || part == MediaName.FileName || part == MediaName.FileNameWithoutExtension) mediaName = _base._fileName;
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
        /// Gets the number of audio tracks in the playing media.
        /// <br/>See also: Player.Media.GetAudioTracks.
        /// </summary>
        public int AudioTrackCount
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioTrackCount;
            }
        }

        /// <summary>
        /// Returns the audio tracks in the playing media.
        /// <br/>See also: Player.Media.AudioTrackCount.
        /// </summary>
        public AudioTrack[] GetAudioTracks()
        {
            return _base.AV_GetAudioTracks();
        }

        /// <summary>
        /// Gets the number of video tracks in the playing media.
        /// <br/>See also: Player.Media.GetVideoTracks.
        /// </summary>
        public int VideoTrackCount
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoTrackCount;
            }
        }

        /// <summary>
        /// Returns the video tracks in the playing media.
        /// <br/>See also: Player.Media.VideoTrackCount.
        /// </summary>
        public VideoTrack[] GetVideoTracks()
        {
            return _base.AV_GetVideoTracks();
        }

        /// <summary>
        /// Gets the original size (width and height in pixels) of the video image of the playing media.
        /// This information, and more, is also available in the track information
        /// <br/>obtained with, for example, Player.Media.GetVideoTracks.
        /// </summary>
        public Size VideoSourceSize
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo ? _base._videoSourceSize : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the video frame rate of the playing media, in frames per second (fps).
        /// This information, and more, is also available in the track information
        /// <br/>obtained with, for example, Player.Media.GetVideoTracks.
        /// </summary>
        public float VideoFrameRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo ? _base._videoFrameRate : 0;
            }
        }

        /// <summary>
        /// Gets or sets the (repeat) start time of the playing media.
        /// <br/>The start time can also be set with the Player.Play method.
        /// <br/>If the start time is changed, chapters playback (if any) will end.
        /// <br/>See also: Player.Media.StopTime.
        /// </summary>
        public TimeSpan StartTime
        {
            get
            {
                _base._lastError = NO_ERROR;
                return TimeSpan.FromTicks(_base._startTime);
            }
            set
            {
                if (!_base._playing || !_base._fileMode)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return;
                }

                //_base._lastError = NO_ERROR;

                if (_base._chapterMode)
                {
                    _base.AV_StopChapters(value, TimeSpan.Zero);
                }
                else
                {
                    long newStart = value.Ticks;

                    if (_base._startTime == newStart) return;
                    if ((_base._stopTime != 0 && newStart >= _base._stopTime) || newStart >= _base._mediaLength)
                    {
                        _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                        return;
                    }

                    if (_base._hasPositionSlider && _base._psHandlesProgress)
                    {
                        _base._positionSlider.Minimum = (int)(newStart * Player.TICKS_TO_MS);
                    }

                    _base._startTime = newStart;
                }

                if (_base._startTime > _base.PositionX) _base.PositionX = _base._startTime;
                _base._mediaStartStopTimeChanged?.Invoke(_base, EventArgs.Empty);

                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the (repeat) stop time of the playing media.
        /// <br/>The stop time can also be set with the Player.Play method.
        /// <br/>TimeSpan.Zero or 00:00:00 indicates the natural end of the media.
        /// <br/>If the stop time is changed, chapters playback (if any) will end. 
        /// <br/>See also: Player.Media.StartTime.
        /// </summary>
        public TimeSpan StopTime
        {
            get
            {
                _base._lastError = NO_ERROR;
                return TimeSpan.FromTicks(_base._stopTime);
            }
            set
            {
                if (!_base._playing || !_base._fileMode)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return;
                }

                //_base._lastError = NO_ERROR;

                if (_base._chapterMode)
                {
                    _base.AV_StopChapters(TimeSpan.Zero, value);
                }
                else
                {
                    long newStop = value.Ticks;

                    if (_base._stopTime == newStop) return;
                    if ((newStop != 0 && newStop < _base._startTime) || newStop >= _base._mediaLength)
                    {
                        _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                        return;
                    }

                    if (_base._hasPositionSlider && _base._psHandlesProgress)
                    {
                        _base._positionSlider.Maximum = newStop == 0 ? (int)(_base._mediaLength * Player.TICKS_TO_MS) : (int)(newStop * Player.TICKS_TO_MS);
                    }

                    _base._stopTime = newStop;
                    _base.AV_UpdateTopology();
                }
                _base._mediaStartStopTimeChanged?.Invoke(_base, EventArgs.Empty);

                _base._lastError = NO_ERROR;
            }
        }


        // Get Metadata

        /// <summary>
        /// Returns metadata (media information such as title and artist name) of the playing media.
        /// <br/>See also: Player.Media.GetAudioTracks and .GetVideoTracks track information.
        /// </summary>
        public Metadata GetMetadata()
        {
            return GetMetadata(ImageSource.MediaOrFolder);
        }

        /// <summary>
        /// Returns metadata (media information such as title and artist name) of the playing media.
        /// <br/>See also: Player.Media.GetAudioTracks and .GetVideoTracks track information.
        /// </summary>
        /// <param name="imageSource">A value indicating whether and where to obtain an image related to the media.</param>
        public Metadata GetMetadata(ImageSource imageSource)
        {
            Metadata tagInfo = null;

            if (_base._playing)
            {
                MediaSourceCategory sourceCategory = _base.AV_GetSourceCategory();

                if (sourceCategory == MediaSourceCategory.LocalFile || sourceCategory == MediaSourceCategory.RemoteFile)
                {
                    tagInfo = GetMediaTags(_base.mf_MediaSource, imageSource);
                    try
                    {
                        if (sourceCategory == MediaSourceCategory.LocalFile)
                        {
                            // Get album art image (with certain values of 'imageSource')
                            if (imageSource == ImageSource.FolderOrMedia || imageSource == ImageSource.FolderOnly || (imageSource == ImageSource.MediaOrFolder && tagInfo._image == null))
                            {
                                GetMediaImage(_base._fileName);
                                if (_tagImage != null) // null image not to replace image retrieved from media file with FolderOrMedia
                                {
                                    tagInfo._image = _tagImage;
                                    _tagImage = null;
                                }
                            }
                        }
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                }
                else if (sourceCategory == MediaSourceCategory.LiveStream)
                {
                    tagInfo = GetMediaTagsLive(_base.mf_MediaSource);
                }

                if (tagInfo == null) tagInfo = new Metadata();
                if (tagInfo._title == null || tagInfo._title.Length == 0) tagInfo._title = Path.GetFileNameWithoutExtension(_base._fileName);
                if (sourceCategory != MediaSourceCategory.LocalFile && (tagInfo._album == null || tagInfo._album.Length == 0)) tagInfo._album = _base._fileName;
            }
            else
            {
                tagInfo = new Metadata();
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }

            return tagInfo;
        }

        /// <summary>
        /// Returns metadata (media information such as title and artist name) of the specified media file.
        /// <br/>See also: Player.Media.GetAudioTracks and .GetVideoTracks track information.
        /// </summary>
        /// <param name="fileName">The path and name of the media file from which the metadata is to be obtained.</param>
        /// <param name="imageSource">A value indicating whether and where to obtain an image related to the media file.</param>
        public Metadata GetMetadata(string fileName, ImageSource imageSource)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                _base._lastError = HResult.E_INVALIDARG;
                return new Metadata();
            }

            Metadata tagInfo;
            _base._lastError = NO_ERROR;

            try
            {
                if (!new Uri(fileName).IsFile)
                {
                    tagInfo = new Metadata();
                    {
                        try
                        {
                            tagInfo._title = Path.GetFileNameWithoutExtension(fileName);
                            tagInfo._album = fileName;
                        }
                        catch { /* ignored */ }
                    }
                    return tagInfo;
                }
            }
            catch { /* ignored */ }

            tagInfo = GetMediaTags(fileName, imageSource);

            try
            {
                // Get info from file path
                if (tagInfo._title == null || tagInfo._title.Length == 0) tagInfo._title = Path.GetFileNameWithoutExtension(fileName);
                if (imageSource == ImageSource.FolderOrMedia || imageSource == ImageSource.FolderOnly || (imageSource == ImageSource.MediaOrFolder && tagInfo._image == null))
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

        private static Metadata GetMediaTags(string fileName, ImageSource imageSource)
        {
            Metadata tagInfo = null;

            HResult result = MFExtern.MFCreateSourceResolver(out IMFSourceResolver sourceResolver);
            if (result == NO_ERROR)
            {
                try
                {
                    result = sourceResolver.CreateObjectFromURL(fileName, MFResolution.MediaSource, null, out MFObjectType objectType, out object source);
                    if (result == NO_ERROR)
                    {
                        tagInfo = GetMediaTags((IMFMediaSource)source, imageSource);
                    }
                }
                catch { /* ignored */ }
            }

            if (sourceResolver != null) Marshal.ReleaseComObject(sourceResolver);

            if (tagInfo == null) tagInfo = new Metadata();
            return tagInfo;
        }

        private static Metadata GetMediaTags(IMFMediaSource mediaSource, ImageSource imageSource)
        {
            Metadata        tagInfo     = new Metadata();
            IPropertyStore  propStore   = null;
            PropVariant     propVariant = new PropVariant();

            HResult result;
            try
            {
                result = MFExtern.MFGetService(mediaSource, MFServices.MF_PROPERTY_HANDLER_SERVICE, typeof(IPropertyStore).GUID, out object store);
                if (result == NO_ERROR)
                {
                    propStore = (IPropertyStore)store;

                    // Artist
                    result = propStore.GetValue(PropertyKeys.PKEY_Music_Artist, propVariant);
                    tagInfo._artist = propVariant.GetString();

                    // Album Artist
                    propStore.GetValue(PropertyKeys.PKEY_Music_AlbumArtist, propVariant);
                    tagInfo._albumArtist = propVariant.GetString();

                    // Title
                    propStore.GetValue(PropertyKeys.PKEY_Title, propVariant);
                    tagInfo._title = propVariant.GetString();

                    // SubTitle
                    propStore.GetValue(PropertyKeys.PKEY_Media_SubTitle, propVariant);
                    tagInfo._subTitle = propVariant.GetString();

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

                    // Comment
                    propStore.GetValue(PropertyKeys.PKEY_Comment, propVariant);
                    tagInfo._comment = propVariant.GetString();

                    // Image
                    if (imageSource != ImageSource.None && imageSource != ImageSource.FolderOnly)
                    {
                        propStore.GetValue(PropertyKeys.PKEY_ThumbnailStream, propVariant);
                        if (propVariant.ptr != null)
                        {
                            IStream stream = (IStream)Marshal.GetObjectForIUnknown(propVariant.ptr);

                            stream.Stat(out System.Runtime.InteropServices.ComTypes.STATSTG streamInfo, STATFLAG.NoName);

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
            //catch (Exception e) { result = (HResult)Marshal.GetHRForException(e); }
            catch { /* ignored */ }

            if (propStore != null) Marshal.ReleaseComObject(propStore);
            propVariant.Dispose();

            return tagInfo;
        }

        private static Metadata GetMediaTagsLive(IMFMediaSource mediaSource)
        {
            Metadata tagInfo = new Metadata();

            PropVariant                 propVariant      = new PropVariant();
            IMFMetadataProvider         metadataProvider = null;
            IMFPresentationDescriptor   descriptor       = null;
            IMFMetadata                 metadata         = null;

            HResult result = MFExtern.MFGetService(mediaSource, MFServices.MF_METADATA_PROVIDER_SERVICE, typeof(IMFMetadataProvider).GUID, out object obj);
            if (result == Player.NO_ERROR)
            {
                metadataProvider = obj as IMFMetadataProvider;
                result = mediaSource.CreatePresentationDescriptor(out descriptor);
                if (result == Player.NO_ERROR)
                {
                    metadataProvider.GetMFMetadata(descriptor, 0, 0, out metadata);

                    metadata.GetAllPropertyNames(propVariant);
                    string[] names = propVariant.GetStringArray();

                    if (names != null)
                    {
                        try
                        {
                            int count = names.Length;
                            for (int i = 0; i < count; i++)
                            {
                                //metadata.GetProperty(names[i], propVariant);
                                //MessageBox.Show(names[i] + ": " + propVariant.GetString());

                                metadata.GetProperty(names[i], propVariant);
                                switch (names[i].ToLower())
                                {
                                    case "author": // "wm /originalartist": // ?
                                        tagInfo._artist = propVariant.GetString();
                                        break;

                                    case "wm/albumartist":
                                        tagInfo._albumArtist = propVariant.GetString();
                                        break;

                                    case "title":
                                        tagInfo._title = propVariant.GetString();
                                        break;

                                    case "wm/subtitle":
                                        tagInfo._subTitle = propVariant.GetString();
                                        break;

                                    case "wm/albumtitle":
                                        tagInfo._album = propVariant.GetString();
                                        break;

                                    case "wm/genre":
                                        tagInfo._genre = propVariant.GetString();
                                        break;

                                    case "releasedateyear": // ?
                                        tagInfo._year = propVariant.GetString(); // string or int?
                                        break;

                                    case "comment":
                                        tagInfo._comment = propVariant.GetString();
                                        break;
                                }
                            }
                        }
                        catch { /* ignored */ }
                    }
                }
            }

            if (metadataProvider != null)   Marshal.ReleaseComObject(metadataProvider);
            if (descriptor != null)         Marshal.ReleaseComObject(descriptor);
            if (metadata != null)           Marshal.ReleaseComObject(metadata);

            propVariant.Dispose();

            return tagInfo;
        }

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

            string  imageFile   = string.Empty;
            long    length      = 0;
            bool    found       = false;

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


        // Set Metadata

        /// <summary>
        /// Adds the specified metadata to the specified media file.
        /// </summary>
        /// <param name="fileName">The path and name of the media file to which the metadata should be added.</param>
        /// <param name="title">The title of the media.</param>
        public int SetMetadata(string fileName, string title)
        {
            if (title == null)
            {
                _base._lastError = HResult.E_INVALIDARG;
                return (int)_base._lastError;
            }
            return SetMetadata(fileName, title, null, null, null, null, 0, null, 0, null);
        }

        /// <summary>
        /// Adds the specified metadata to the specified media file.
        /// </summary>
        /// <param name="fileName">The path and name of the media file to which the metadata should be added.</param>
        /// <param name="title">The title of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="albumArtist">The name(s) of the primary artist(s) of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// /// <param name="artist">The name(s) of the artist(s) of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        public int SetMetadata(string fileName, string title, string albumArtist, string artist)
        {
            if (title == null && albumArtist == null && artist == null)
            {
                _base._lastError = HResult.E_INVALIDARG;
                return (int)_base._lastError;
            }
            return SetMetadata(fileName, title, null, albumArtist, artist, null, 0, null, 0, null);
        }

        /// <summary>
        /// Adds the specified metadata to the specified media file.
        /// </summary>
        /// <param name="fileName">The path and name of the media file to which the metadata should be added.</param>
        /// <param name="title">The title of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="subTitle">The subtitle of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="albumArtist">The name(s) of the primary artist(s) of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="artist">The name(s) of the artist(s) of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="albumTitle">The album title of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="trackNumber">The track number of the media.
        /// <br/>The value 0 (zero) leaves this item unchanged.</param>
        /// <param name="genre">The genre of the media.
        /// <br/>The value null leaves this item unchanged.</param>
        /// <param name="year">The year the media was published.
        /// <br/>The value 0 (zero) leaves this item unchanged.</param>
        /// <param name="comment">The comment attached to the media.
        /// <br/>The value null leaves this item unchanged.</param>
        public int SetMetadata(string fileName, string title, string subTitle, string albumArtist, string artist, string albumTitle, int trackNumber, string genre, int year, string comment)
        {
            Guid            iid     = typeof(IPropertyStore).GUID;
            IPropertyStore  store   = null;

            if (string.IsNullOrWhiteSpace(fileName)) _base._lastError = HResult.ERROR_INVALID_NAME;
            else if (!File.Exists(fileName)) _base._lastError = HResult.ERROR_FILE_NOT_FOUND;
            else
            {
                try
                {
                    _base._lastError = MFExtern.SHGetPropertyStoreFromParsingName(fileName, IntPtr.Zero, GETPROPERTYSTOREFLAGS.GPS_READWRITE, ref iid, out store);
                    if (_base._lastError == NO_ERROR)
                    {
                        if (title != null) // allow spaces to wipe out
                        {
                            PropVariant prop = new PropVariant(title);
                            store.SetValue(PropertyKeys.PKEY_Title, prop);
                            prop.Dispose();
                        }

                        if (subTitle != null)
                        {
                            PropVariant prop = new PropVariant(subTitle);
                            store.SetValue(PropertyKeys.PKEY_Media_SubTitle, prop);
                            prop.Dispose();
                        }

                        if (albumArtist != null)
                        {
                            PropVariant prop = new PropVariant(albumArtist);
                            store.SetValue(PropertyKeys.PKEY_Music_AlbumArtist, prop);
                            prop.Dispose();
                        }

                        if (artist != null)
                        {
                            PropVariant prop = new PropVariant(artist);
                            store.SetValue(PropertyKeys.PKEY_Music_Artist, prop);
                            prop.Dispose();
                        }

                        if (albumTitle != null)
                        {
                            PropVariant prop = new PropVariant(albumTitle);
                            store.SetValue(PropertyKeys.PKEY_Music_AlbumTitle, prop);
                            prop.Dispose();
                        }

                        if (trackNumber > 0)
                        {
                            PropVariant prop = new PropVariant(trackNumber);
                            store.SetValue(PropertyKeys.PKEY_Music_TrackNumber , prop);
                            prop.Dispose();
                        }

                        if (genre != null)
                        {
                            PropVariant prop = new PropVariant(genre);
                            store.SetValue(PropertyKeys.PKEY_Music_Genre, prop);
                            prop.Dispose();
                        }

                        if (year > 0) // whatever you want
                        {
                            PropVariant prop = new PropVariant(year);
                            store.SetValue(PropertyKeys.PKEY_Media_Year, prop);
                            prop.Dispose();
                        }

                        if (comment != null)
                        {
                            PropVariant prop = new PropVariant(comment);
                            store.SetValue(PropertyKeys.PKEY_Comment, prop);
                            prop.Dispose();
                        }

                        store.Commit();
                    }
                }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

                if (store != null) Marshal.ReleaseComObject(store);
            }
            return (int)_base._lastError;
        }

    }
}